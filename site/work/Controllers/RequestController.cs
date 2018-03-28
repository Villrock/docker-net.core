using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QFlow.Data.Managers;
using QFlow.Extensions;
using QFlow.Helper;
using QFlow.Models;
using QFlow.Models.DataModels;
using QFlow.Models.DataModels.Requests;
using QFlow.Models.HomeViewModals;
using QFlow.Services;

namespace QFlow.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly RequestManager _requestManager;
        private readonly SettingsService _settings;
        private readonly NotificationHelper _notificationHelper;
        private readonly FileService _fileService;
        private readonly string _brocureFolderPath;
        private readonly IHostingEnvironment _hostingEnvironment;

        public RequestController(
            RequestManager requestManager,
            SettingsService settings,
            NotificationHelper notificationHelper,
            FileService fileService,
            IHostingEnvironment environment)
        {
            _requestManager = requestManager;
            _settings = settings;
            _notificationHelper = notificationHelper;
            _fileService = fileService;
            _hostingEnvironment = environment;
            _brocureFolderPath = Path.Combine(environment.WebRootPath, Consts.BROCHURE_FOLDER_NAME);
        }

        public IActionResult Index(int page = 1, string status = "")
        {
            ViewData["ActiveStatusName"] = string.IsNullOrEmpty(status)
                                            ? Consts.ALL
                                            : status;

            var viewModel = new RequestPageViewModel
            {
                IsManager = User.IsManager()
            };
            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region CRUD Request

        [HttpGet]
        public async Task<JsonResult> List(int page = 1, string status = "", string search = "")
        {
            search = search ?? string.Empty;

            var startIndex = (page - 1) * _settings.PageSize;
            var statusId = (await _requestManager.GetStatusByNameAsync(status))?.Id ?? -1;
            var requests = await _requestManager.GetRequestsPageAsync(User, startIndex, _settings.PageSize, statusId, search);
            var totalCount = await _requestManager.GetRequestCountAsync(User, statusId, search);

            return Json(new
            {
                IsManager = User.IsManager(),
                Requests = from request in requests
                           select BuildRequestViewModel(request),
                PagingInfo = _settings.GetPagingInfo(page, startIndex, totalCount, Request.ContentUrl())
            });
        }

        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            var request = await _requestManager.GetRequestByIdAsync(id, User);
            if(request == null)
            {
                throw new InvalidDataException($"Request (id = {id}) not found.");
            }
            var availableStatuses = User.IsManager()
                ? await _requestManager.GettAvailableStatusesAsync(request.RequestStatusId)
                : await _requestManager.GetClientAvailableStatusesAsync(request.RequestStatusId);

            return Json(new
            {
                Request = BuildFullRequestViewModel(request),
                Details = await BuildRequestDetailsViewModel(request),
                Statuses = from st in availableStatuses
                           select new
                           {
                               st.Id,
                               st.Name
                           },
                CurrentRequestStatus = request.RequestStatusId
            });
        }

        [HttpPost]
        public async Task New(RequestViewModel model)
        {
            var request = await _requestManager.NewAsync(MakeRequestLoader(model), MakeRequestDetailsLoader(model.Details), User);
            await ProcessingNotificationAndAlerts(new[] { request });
        }

        [HttpPost]
        public async Task Set(RequestViewModel model)
        {
            var oldRequest = await _requestManager.GetRequestByIdAsync(model.Id);
            var isUpdateAlertsByEstimateDeliveryDate = false;
            var isUpdatedStatus = false;
            var oldStutus = 0;
            DateTime? oldEstimateDeliveryDate = null;

            if(oldRequest != null)
            {
                oldStutus = oldRequest.RequestStatusId;
                isUpdatedStatus = model.Status != oldRequest.RequestStatusId;
                isUpdateAlertsByEstimateDeliveryDate = oldRequest.RequestStatusId >= 30;

                var details = await _requestManager.GetRequestDetailsByRequestId(oldRequest.Id);
                oldEstimateDeliveryDate = details.FirstOrDefault()?.EstimateDeliveryDate;
            }

            var requests = await _requestManager.UpdateAsync(model.Id, MakeRequestLoader(model), MakeRequestDetailsLoader(model.Details), User);

            await ProcessingNotificationAndAlerts(requests, isUpdatedStatus, oldStutus);

            if(isUpdateAlertsByEstimateDeliveryDate && User.IsManager())
            {
                await CreateOrUpdateAlertsByEstimateDeliveryDate(requests, oldEstimateDeliveryDate);
            }
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            await _requestManager.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<JsonResult> GetCurrencies(string search)
        {
            var results = from result in await _requestManager.FindCurrenciesNameAsync(search)
                          select new
                          {
                              result.Name
                          };
            return Json(results);
        }

        [HttpGet]
        public async Task<JsonResult> GetManufacturers(string search)
        {
            var results = from result in await _requestManager.FindManufacturerByNameAsync(search)
                          select new
                          {
                              result.Name
                          };
            return Json(results);
        }

        [HttpGet]
        public async Task<JsonResult> GetManufacturerModels(string search, string searchManufacture)
        {
            var models = await _requestManager.FindManufacturerModelByNameAsync(search, searchManufacture);
            var results = from result in models
                          select new
                          {
                              result.Name
                          };
            return Json(results);
        }

        [HttpPost]
        public async Task SaveFiles(FileUploadViewModel model)
        {
            var index = 0;
            var arrayPaths = model.FilePaths.ToArray();
            foreach(var file in model.Files)
            {
                if(file != null)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, arrayPaths[index]);
                    await _fileService.Save(file, path);
                }
                index++;
            }
        }

        #endregion

        #region Implementation routines

        private async Task ProcessingNotificationAndAlerts(IEnumerable<Request> requests, bool isUpdatedStatus = true, int oldStatus = 0)
        {
            var uri = HttpContext.Request.GetUri();
            foreach(var request in requests)
            {
                var status = await _requestManager.GetStatusByIdAsync(request.RequestStatusId);
                var link = $"{uri.Scheme}://{uri.Host}:{uri.Port}/{(status == null ? "" : "?status=" + status.Name)}#request={request.Id}";

                var isReturnedStatus = request.RequestStatusId < oldStatus;
                if(isUpdatedStatus)
                {
                    await _notificationHelper.ProcessingNotificationAndAlert(request, link, isReturnedStatus);
                }
                else
                {
                    await _notificationHelper.UpdateAlertsByRequest(request, link);
                }
            }
        }

        private async Task CreateOrUpdateAlertsByEstimateDeliveryDate(IEnumerable<Request> requests, DateTime? oldEstimateDeliveryDate)
        {
            var uri = HttpContext.Request.GetUri();
            var request = requests.FirstOrDefault();
            if(request == null)
                return;

            var link = $"{uri.Scheme}://{uri.Host}:{uri.Port}/#request={request.Id}";
            var detail = requests.FirstOrDefault()?.Details.FirstOrDefault();

            var isUpdateAlerts = detail?.EstimateDeliveryDate != null
                                && (oldEstimateDeliveryDate != null
                                && detail.EstimateDeliveryDate.Value != oldEstimateDeliveryDate.Value);
            if(isUpdateAlerts)
                await _notificationHelper.CreateAlertsByEstimateDeliveryDate(request, link);
        }

        private static EditRequestViewModel BuildRequestViewModel(Request request)
        {
            return new EditRequestViewModel
            {
                Id = request.Id,
                RequestNumber = request.RequestNumber,
                Title = request.Title,
                ClientName = request.Client?.ToString(),
                Created = request.CreatedAsString,
                Status = request.RequestStatus.Id,
                StatusText = request.RequestStatus.Name,
            };
        }

        private async Task<dynamic> BuildRequestDetailsViewModel(Request request)
        {
            var details = from detail in await _requestManager.GetRequestDetailsByRequestId(request.Id)
                          select BuildFullRequestDetailViewModel(request.RequestStatusId, detail);
            if(!User.IsManager())
            {
                return details.ToClientModelList();
            }
            return details;
        }

        private EditRequestViewModel BuildFullRequestViewModel(Request request)
        {
            var model = BuildRequestViewModel(request);
            model.DateStudy = request.DateOfStudy;
            model.DurationStudy = request.DurationOfStudy;
            model.FullAddress = request.FullAddress;
            model.SpecialInstructions = request.SpecialInstructions;
            model.StudyInformation = request.StudyInformation;
            model.RequestNumber = request.RequestNumber;

            return model;
        }

        private RequestDetailViewModel BuildFullRequestDetailViewModel(int statusId, RequestDetail detail)
        {
            var isManager = User.IsManager();
            var model = new RequestDetailViewModel
            {
                Id = detail.Id,
                Manufacturer = _requestManager.GetManufacturerByRequestDetailId(detail.Id)?.Name,
                Model = _requestManager.GetManufacturerModelByRequestDetailId(detail.Id)?.Name,
                ServiceWarrantyOptions = detail.ServiceWarrantyOptions,
                AncillaryItems = detail.AncillaryItems,
                Quantity = detail.Quantity
            };

            var resourcePath = Path.Combine(_hostingEnvironment.WebRootPath, Consts.RESOURCES_FOLDER_NAME + "/" + detail.FolderId);
            /*Additional Fields - QUOTED*/
            if(statusId >= 20 || (statusId >= 10 && isManager))
            {
                model.Currency = detail.Currency;
                model.UnitPrice = detail.UnitPrice;
                model.ShippingDate = detail.ShippingDate;
                model.TrainingDate = detail.TrainingDate;
                model.InstallationDate = detail.InstallationDate;
                model.EstimatedLeadTime = detail.EstimatedLeadTime;
                model.CalibrationServiceRequirements = detail.CalibrationServiceRequirements;
                model.AlternativesRecommendation = detail.AlternativesRecommendation;
                model.LocalTechnicalSupport = detail.LocalTechnicalSupport;
                model.HasApprovalsCertificationRequired = !string.IsNullOrEmpty(detail.ApprovalsCertificationRequired)
                                                          && _fileService.HasFile(detail.ApprovalsCertificationRequired, resourcePath);
                if(model.HasApprovalsCertificationRequired)
                    model.ApprovalsCertificationRequired = detail.ApprovalsCertificationRequired;

                model.HasLinkToBrochure = !string.IsNullOrEmpty(detail.LinkToBrochure)
                                          && _fileService.HasFile(detail.LinkToBrochure, _brocureFolderPath);
                if(model.HasLinkToBrochure)
                    model.LinkToBrochure = detail.LinkToBrochure;
            }

            /*Additional Fields - PURCHASE ORDER*/
            if(statusId >= 30 || (statusId >= 20 && isManager))
            {
                model.SivRequiredDate = detail.SivRequiredDate;
                model.ContactPersons = detail.ContactPersons;
                model.SiteActivated = detail.SiteActivated;
                model.DeInstallDueDate = detail.DeInstallDueDate;
                model.ServiceDueDate = detail.ServiceDueDate;

                //Shipping and Delivery Details
                model.TrackingNumberOrCourierDetails = detail.TrackingNumberOrCourierDetails;
                model.NameOfPerson = detail.NameOfPerson;
                model.ConfirmedDeliveryDate = detail.ConfirmedDeliveryDate;
                model.ActualDespatchDate = detail.ActualDespatchDate;
                model.ActualInstallationDate = detail.ActualInstallationDate;

                model.HasTrainingDocuments = !string.IsNullOrEmpty(detail.TrainingDocuments)
                                             && _fileService.HasFile(detail.TrainingDocuments, resourcePath);
                if(model.HasTrainingDocuments)
                    model.TrainingDocuments = detail.TrainingDocuments;

                model.HasProofOfDelivery = !string.IsNullOrEmpty(detail.ProofOfDelivery)
                                           && _fileService.HasFile(detail.ProofOfDelivery, resourcePath);
                if(model.HasProofOfDelivery)
                    model.ProofOfDelivery = detail.ProofOfDelivery;

                model.SerialNumber = detail.SerialNumber;
                model.EstimateDeliveryDate = detail.EstimateDeliveryDate;
                model.HasInvoiceFile = !string.IsNullOrEmpty(detail.InvoiceFile)
                                           && _fileService.HasFile(detail.InvoiceFile, resourcePath);
                if(model.HasInvoiceFile)
                    model.InvoiceFile = detail.InvoiceFile;
            }

            /*Additional Fields - INVOICED*/
            if(statusId >= 40 || (statusId >= 30 && isManager))
            {
                model.PaymentDueDate = detail.PaymentDueDate;
                model.IssuedDate = detail.IssuedDate;
                model.PaymentRunDate = detail.PaymentRunDate;
            }

            /*Additional Fields - PAID */
            if(statusId == 50 || (statusId >= 40 && isManager))
            {
                model.PaymentReceivedDate = detail.PaymentReceivedDate;
            }

            model.FolderId = detail.FolderId ?? Guid.NewGuid();

            return model;
        }

        private static Func<Request, bool> MakeRequestLoader(RequestViewModel model)
        {
            if(model == null)
            {
                return request => false;
            }
            return request =>
            {
                request.Title = model.Title;
                request.RequestStatusId = model.Status;

                request.DateOfStudy = model.DateStudy;
                request.DurationOfStudy = model.DurationStudy;
                request.FullAddress = model.FullAddress;
                request.SpecialInstructions = model.SpecialInstructions;
                request.StudyInformation = model.StudyInformation;
                request.CreatedAsString = request.CreatedAsString ?? request.Created.ToString("yyyy-MM-dd");

                return true;
            };
        }

        private Func<Request, Task<bool>> MakeRequestDetailsLoader(IEnumerable<RequestDetailViewModel> detailViewModels)
        {
            return async request =>
            {
                var isManager = User.IsManager();
                var details = await _requestManager.GetRequestDetailsByRequestId(request.Id);
                foreach(var detail in details)
                {
                    detail.RequestId = default(int?);
                }
                foreach(var model in detailViewModels)
                {
                    var entity = await _requestManager.GetRequestDetailAsync(model.Id);
                    var manufacturer = await _requestManager.GetManufacturerByNameAsync(model.Manufacturer, entity);
                    var manufacturerModel = await _requestManager.GetManufacturerModelByNameAsync(model.Model, manufacturer, entity);

                    entity.ServiceWarrantyOptions = model.ServiceWarrantyOptions;
                    entity.AncillaryItems = model.AncillaryItems;
                    entity.Manufacturer = manufacturer;
                    entity.ManufacturerModel = manufacturerModel;
                    entity.RequestId = request.Id;
                    entity.Quantity = model.Quantity;

                    if(request.RequestStatusId >= 20 || (request.RequestStatusId >= 10 && isManager))
                    {
                        /*Additional Fields - QUOTED*/
                        var currency = await _requestManager.GetCurrencyByName(model.Currency);
                        entity.Currency = currency != null ? currency.Name : "";
                        entity.UnitPrice = model.UnitPrice;
                        entity.ShippingDate = model.ShippingDate;
                        entity.TrainingDate = model.TrainingDate;
                        entity.InstallationDate = model.InstallationDate;
                        entity.EstimatedLeadTime = model.EstimatedLeadTime;
                        entity.CalibrationServiceRequirements = model.CalibrationServiceRequirements;
                        entity.AlternativesRecommendation = model.AlternativesRecommendation;
                        entity.LocalTechnicalSupport = model.LocalTechnicalSupport;
                        entity.ApprovalsCertificationRequired = model.ApprovalsCertificationRequired;
                        entity.LinkToBrochure = string.IsNullOrEmpty(model.LinkToBrochure)
                                                        ? model.LinkToBrochure
                                                        : _fileService.HasFile(model.LinkToBrochure, _brocureFolderPath)
                                                                ? model.LinkToBrochure
                                                                : entity.LinkToBrochure;
                    }

                    if(request.RequestStatusId >= 30 || (request.RequestStatusId >= 20 && isManager))
                    {
                        /*Additional Fields - PURCHASE ORDER*/
                        if(User.IsManager())
                        {
                            entity.SivRequiredDate = model.SivRequiredDate;
                            entity.ContactPersons = model.ContactPersons;
                            entity.SiteActivated = model.SiteActivated;
                            entity.DeInstallDueDate = model.DeInstallDueDate;
                            entity.ServiceDueDate = model.ServiceDueDate;
                        }

                        //Shipping and Delivery Details
                        if(User.IsManager())
                        {
                            entity.NameOfPerson = model.NameOfPerson;
                            entity.ConfirmedDeliveryDate = model.ConfirmedDeliveryDate;
                            entity.ActualDespatchDate = model.ActualDespatchDate;
                            entity.ActualInstallationDate = model.ActualInstallationDate;
                        }
                        entity.TrackingNumberOrCourierDetails = model.TrackingNumberOrCourierDetails;
                        entity.TrainingDocuments = model.TrainingDocuments;
                        entity.ProofOfDelivery = model.ProofOfDelivery;
                        entity.SerialNumber = model.SerialNumber;
                        entity.InvoiceFile = model.InvoiceFile;
                        entity.EstimateDeliveryDate = model.EstimateDeliveryDate;
                    }
                    if(request.RequestStatusId >= 40 || (request.RequestStatusId >= 30 && isManager))
                    {
                        /*Additional Fields - INVOICED*/
                        if(User.IsManager())
                        {
                            entity.IssuedDate = model.IssuedDate;
                        }
                        entity.PaymentDueDate = model.PaymentDueDate;
                        entity.PaymentRunDate = model.PaymentRunDate;

                        /*Additional Fields - PAID */
                        if(!model.PaymentReceivedDate.HasValue && request.RequestStatusId == 50)
                        {
                            entity.PaymentReceivedDate = DateTime.UtcNow;
                        }
                        else if(model.PaymentReceivedDate.HasValue)
                        {
                            entity.PaymentReceivedDate = model.PaymentReceivedDate;
                        }

                        entity.FolderId = entity.FolderId ?? model.FolderId;
                    }
                }
                return true;
            };
        }

        #endregion
    }
}
