using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QFlow.Extensions;
using QFlow.Helper;
using QFlow.Models.DataModels;
using QFlow.Models.DataModels.Requests;
using QFlow.Models.DataModels.Users;

namespace QFlow.Data.Managers
{
    public class RequestManager
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _contextDb;
        private readonly UserManager _userManager;

        public RequestManager(ApplicationDbContext context, ILogger<RequestManager> logger, UserManager userManager)
        {
            _contextDb = context;
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        ///  Get Request by id
        /// </summary>
        public async Task<Request> GetRequestByIdAsync(int id, IPrincipal user)
        {
            _logger.LogInformation("Get request by id (id={0})", id);

            var isManager = user.IsManager();
            var client = isManager ? null : await _userManager.GetClientByEmailAsync(user.Identity.Name);

            return await _contextDb.Requests
                                    .Include(_ => _.Client)
                                    .Include(_ => _.RequestStatus)
                                        .FirstOrDefaultAsync(_ => _.Id == id
                                                                && (isManager || (client != null && client.Id == _.ClientId))
                                                                && _.RequestStatusId < 100);
        }

        /// <summary>
        ///  Get Request by id
        /// </summary>
        public async Task<Request> GetRequestByIdAsync(int id)
        {
            _logger.LogInformation("Get request by id (id={0})", id);

            return await _contextDb.Requests.FindAsync(id);
        }

        /// <summary>
        /// Get requests by filter and pagging
        /// </summary>
        public async Task<IEnumerable<Request>> GetRequestsPageAsync(IPrincipal user, int startIndex, int pageCount, int statusId, string search = "")
        {
            search = string.IsNullOrEmpty(search) ? string.Empty : search.Trim().ToLower();
            var isManager = user.IsManager();
            if(isManager)
            {
                return await GetRequestsAsync(true, search, statusId, null, startIndex, pageCount);
            }

            var client = await _userManager.GetClientByEmailAsync(user.Identity.Name);
            return client == null 
                ? Enumerable.Empty<Request>() 
                : await GetRequestsAsync(false, search, statusId, client, startIndex, pageCount);
        }

        /// <summary>
        /// Create Request
        /// </summary>
        public async Task<Request> NewAsync(
                                            Func<Request, bool> makeRequest,
                                            Func<Request, Task<bool>> makeRequestDetailsLoader,
                                            IPrincipal user)
        {
            if(makeRequest == null)
            {
                throw new ArgumentNullException($"Error! Parameter '{nameof(makeRequest)}' must be not null.");
            }

            _logger.LogInformation("Creting new request.");

            var dateNow = DateTime.UtcNow;
            var newRequest = new Request
            {
                Id = 0,
                Created = dateNow,
                CreatedAsString = dateNow.ToString("yyyy-MM-dd"),
                Client = await _userManager.GetClientByEmailAsync(user.Identity.Name),
                RequestStatus = await GetNewStatusAsync()
            };
            _logger.LogInformation("Creting new request detail.");

            var isMadeRequest = makeRequest(newRequest);
            if(isMadeRequest)
            {
                await _contextDb.Requests.AddAsync(newRequest);
                await _contextDb.SaveChangesAsync();
                _logger.LogInformation("Created new request (id = {0}).", newRequest.Id);
            }

            await UpdateDetailsRequest(newRequest, makeRequestDetailsLoader);

            return newRequest;
        }

        /// <summary>
        /// Update request
        /// </summary>
        public async Task<IEnumerable<Request>> UpdateAsync(
                                                int id,
                                                Func<Request, bool> makeRequest,
                                                Func<Request, Task<bool>> makeRequestDetailsLoader,
                                                IPrincipal user)
        {
            var request = await _contextDb.Requests.FirstOrDefaultAsync(_ => _.Id == id);
            if(request == null || makeRequest == null || makeRequestDetailsLoader == null)
            {
                return null;
            }
            await UpdateRequest(request, makeRequest, user);
            await UpdateDetailsRequest(request, makeRequestDetailsLoader);

            var hasEmptyRequestDetails = (await _contextDb.RequestDetails.FirstOrDefaultAsync(_ => _.Request == null)) != null;
            if(hasEmptyRequestDetails)
            {
                var deteils = await _contextDb.RequestDetails.Where(_ => _.Request == null).ToListAsync();
                _contextDb.RequestDetails.RemoveRange(deteils);
                await _contextDb.SaveChangesAsync();
            }
            var isSplitRequest = request.RequestStatusId == 999;
            if(isSplitRequest)
            {
                return await SplitRequest(request);
            }
            return new[] { request };
        }

        /// <summary>
        /// Update request
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var request = await _contextDb.Requests.FindAsync(id);
            if(request == null)
            {
                return;
            }

            _contextDb.Requests.Remove(request);
            await _contextDb.SaveChangesAsync();
        }

        /// <summary>
        /// Get count of filtered requests
        /// </summary>
        public async Task<int> GetRequestCountAsync(IPrincipal user, int statusId, string search = "")
        {
            var isManager = user.IsManager();
            Client client = null;
            if(!isManager)
            {
                client = await _userManager.GetClientByEmailAsync(user.Identity.Name);
            }
            return await _contextDb.Requests
                                    .Include(_ => _.Client)
                                        .Where(_ => (_.RequestStatusId == statusId || statusId == -1)
                                                    && _.RequestStatusId < 100
                                                    && (isManager || (client != null && _.ClientId == client.Id))
                                                    && (string.IsNullOrEmpty(search)
                                                        || _.Id.ToString() == search
                                                        || _.Title.Contains(search)
                                                        || (!string.IsNullOrEmpty(_.CreatedAsString) && _.CreatedAsString.Contains(search))
                                                        || (isManager && (_.Client.FirstName + " " + _.Client.LastName).Contains(search))))
                                    .CountAsync();
        }

        /// <summary>
        /// Return Stutus Entity. By default, return New status
        /// </summary>
        public async Task<RequestStatus> GetStatusByNameAsync(string status)
        {
            return await _contextDb.RequestStatuses.FirstOrDefaultAsync(_ => _.Name == status);
        }

        /// <summary>
        /// Return Stutus Entity
        /// </summary>
        public async Task<RequestStatus> GetStatusByIdAsync(int id)
        {
            return await _contextDb.RequestStatuses.FindAsync(id);
        }

        /// <summary>
        /// Return all Stutuses 
        /// </summary>
        public async Task<IEnumerable<RequestStatus>> GetAllStatusesAsync()
        {
            return await _contextDb.RequestStatuses.Where(_ => _.Id < 100).ToListAsync();
        }

        /// <summary>
        /// Return all Stutuses 
        /// </summary>
        public async Task<IEnumerable<RequestStatus>> GetStatusesWithAllAsync()
        {
            var statuses = await _contextDb.RequestStatuses.Where(_ => _.Id < 100).ToListAsync();
            statuses.Insert(0, new RequestStatus
            {
                Id = -1,
                Name = Consts.ALL
            });
            return statuses;
        }

        /// <summary>
        /// Return available Stutuses
        /// </summary>
        public async Task<IEnumerable<RequestStatus>> GetClientAvailableStatusesAsync(int currentStatusId)
        {
            return await _contextDb.RequestStatuses
                                        .Where(_ => _.Id >= currentStatusId)
                                        .Take(2)
                                            .Where(_ => currentStatusId == 40 || _.Id != 40)
                                            .Where(_ => _.Id < 100)
                                            .ToListAsync();
        }

        /// <summary>
        /// Return available Stutuses
        /// </summary>
        public async Task<IEnumerable<RequestStatus>> GettAvailableStatusesAsync(int currentStatusId)
        {
            int takeCount = currentStatusId == 20 || currentStatusId == 40 ? 3 : 2;
            if(currentStatusId < 30)
            {
                return await _contextDb.RequestStatuses.Where(_ => _.Id <= 30).Take(takeCount).ToListAsync();
            }
            if(currentStatusId == 50)
            {
                return await _contextDb.RequestStatuses.Where(_ => _.Id >= 30 && _.Id < 100).OrderByDescending(_ => _.Id).Take(takeCount).ToListAsync();
            }
            return await _contextDb.RequestStatuses.Where(_ => _.Id >= 30 && _.Id < 100).Take(takeCount).ToListAsync();
        }

        /// <summary>
        /// Return Manufacturer Entity
        /// </summary>
        public async Task<Manufacturer> GetManufacturerByNameAsync(string name, RequestDetail requestDetail)
        {
            var entity = await _contextDb.Manufacturers.FirstOrDefaultAsync(_ => _.Name == name.Trim());
            if(entity != null)
            {
                return entity;
            }

            entity = new Manufacturer
            {
                Id = 0,
                Name = name
            };
            await _contextDb.Manufacturers.AddAsync(entity);
            if(requestDetail.Manufacturer != null)
            {
                var hasOnceManufacturerInRequest = await _contextDb.RequestDetails.CountAsync(_ => _.Manufacturer.Id == requestDetail.Manufacturer.Id) > 1;
                if(hasOnceManufacturerInRequest)
                {
                    _contextDb.Manufacturers.Remove(requestDetail.Manufacturer);
                    requestDetail.Manufacturer = null;
                }
            }
            return entity;
        }

        /// <summary>
        /// Return Manufacturers by name
        /// </summary>
        public async Task<IEnumerable<Manufacturer>> FindManufacturerByNameAsync(string name)
        {
            return await _contextDb.Manufacturers.Where(_ => _.Name.Contains(name)).ToListAsync();
        }

        /// <summary>
        /// Return ManufacturerModels Entity by name and manufacturer
        /// </summary>
        public async Task<IEnumerable<ManufacturerModel>> FindManufacturerModelByNameAsync(string modelName, string manufacture)
        {
            var manufactureEntity = await _contextDb.Manufacturers.FirstOrDefaultAsync(_ => String.Equals(_.Name, manufacture, StringComparison.CurrentCultureIgnoreCase));
            if(manufactureEntity != null)
            {
                return await _contextDb.ManufacturerModels
                                    .Where(_ => _.Name.Contains(modelName) && _.Manufacturer.Id == manufactureEntity.Id)
                                    .ToListAsync();
            }
            return Enumerable.Empty<ManufacturerModel>();
        }

        /// <summary>
        /// Return ManufacturerModel Entity
        /// </summary>
        public async Task<ManufacturerModel> GetManufacturerModelByNameAsync(string name, Manufacturer manufacturer, RequestDetail requestDetail)
        {
            var entity = await _contextDb.ManufacturerModels.FirstOrDefaultAsync(_ => _.Name == name.Trim() && _.Manufacturer.Id == manufacturer.Id);
            if(entity != null)
            {
                return entity;
            }

            entity = new ManufacturerModel
            {
                Id = 0,
                Name = name,
                Manufacturer = manufacturer
            };
            await _contextDb.ManufacturerModels.AddAsync(entity);
            if(requestDetail.ManufacturerModel != null)
            {
                var hasOnceManufacturerInRequest = await _contextDb.RequestDetails.CountAsync(_ => _.ManufacturerModel.Id == requestDetail.ManufacturerModel.Id) > 1;
                if(hasOnceManufacturerInRequest)
                {
                    _contextDb.ManufacturerModels.Remove(requestDetail.ManufacturerModel);
                    requestDetail.ManufacturerModel = null;
                }
            }
            return entity;
        }

        /// <summary>
        /// Return RequestDetail Entity
        /// </summary>
        public async Task<IEnumerable<RequestDetail>> GetRequestDetailsByRequestId(int requestId)
        {
            return await _contextDb.RequestDetails.Where(_ => _.RequestId == requestId).ToListAsync();
        }

        /// <summary>
        /// Return RequestDetail Entity
        /// </summary>
        public async Task<RequestDetail> GetRequestDetailAsync(int id)
        {
            var entity = await _contextDb.RequestDetails.FindAsync(id);
            if(entity == null) // create Entity
            {
                entity = new RequestDetail
                {
                    Id = 0,
                    FolderId = Guid.NewGuid()
                };
                await _contextDb.RequestDetails.AddAsync(entity);
            }
            return entity;
        }

        /// <summary>
        /// Return Manufacturer Entity by detail id
        /// </summary>
        public Manufacturer GetManufacturerByRequestDetailId(int detailId)
        {
            var entity = _contextDb.RequestDetails.Include(_ => _.Manufacturer).FirstOrDefault(_ => _.Id == detailId);
            return entity?.Manufacturer;
        }

        /// <summary>
        /// Return Manufacturer Entity by detail id
        /// </summary>
        public ManufacturerModel GetManufacturerModelByRequestDetailId(int detailId)
        {
            var entity = _contextDb.RequestDetails.Include(_ => _.ManufacturerModel).FirstOrDefault(_ => _.Id == detailId);
            return entity?.ManufacturerModel;
        }

        /// <summary>
        /// Return Manufacturer Entity by detail id
        /// </summary>
        public async Task RemoveUnNecessaryRequestDetaiils(IEnumerable<RequestDetail> details)
        {
            _contextDb.RequestDetails.RemoveRange(details);
            await _contextDb.SaveChangesAsync();
        }

        /// <summary>
        /// Get 'New request' Status
        /// </summary>
        public async Task<RequestStatus> GetNewStatusAsync()
        {
            return await _contextDb.RequestStatuses.FirstAsync();
        }

        /// <summary>
        /// Return Manufacturers by name
        /// </summary>
        public async Task<IEnumerable<Currency>> FindCurrenciesNameAsync(string name)
        {
            return await _contextDb.Currencies.Where(_ => _.Name.Contains(name))
                                              //.Take(10)
                                              .ToListAsync();
        }

        public async Task<Currency> GetCurrencyByName(string name)
        {
            return await _contextDb.Currencies.FirstOrDefaultAsync(_ => _.Name.Contains(name));
        }

        #region Implementation routines

        private async Task<IEnumerable<Request>> GetRequestsAsync(bool isManager, string search, int statusId, Client client, int startIndex = 0, int pageCount = 10)
        {
            return await _contextDb.Requests
                        .Include(_ => _.Client)
                        .Include(_ => _.RequestStatus)
                           .Where(_ => (_.RequestStatusId == statusId || statusId == -1)
                                       && _.RequestStatusId < 100
                                       && (isManager || (client != null && _.ClientId == client.Id))
                                       && (string.IsNullOrEmpty(search)
                                            || _.Id.ToString() == search
                                            || _.RequestNumber.Contains(search)
                                            || _.Title.Contains(search)
                                            || (!string.IsNullOrEmpty(_.CreatedAsString) &&_.CreatedAsString.Contains(search))
                                            || (isManager && (_.Client.FirstName + " " + _.Client.LastName).Contains(search))))
                            .OrderByDescending(_ => _.Created)
                            .Skip(startIndex)
                            .Take(pageCount)
                                .ToListAsync();
        }

        private async Task<IEnumerable<Request>> SplitRequest(Request request)
        {
            var newRequests = new List<Request>();
            var details = await GetRequestDetailsByRequestId(request.Id);
            var index = 1;
            foreach(var requestDetail in details)
            {
                //create new request clone
                var newRequest = request.Clone();

                newRequest.RequestStatusId = 30; // Purchase Order
                newRequest.RequestNumber = request.Id + "-" + index;
                newRequest.RequestId = request.Id;

                await _contextDb.Requests.AddAsync(newRequest);
                await _contextDb.SaveChangesAsync();

                //create new request detail clone
                var newRequestDetail = requestDetail.Clone();
                newRequestDetail.RequestId = newRequest.Id;
                await _contextDb.RequestDetails.AddAsync(newRequestDetail);
                await _contextDb.SaveChangesAsync();

                index++;
                newRequests.Add(newRequest);
            }
            return newRequests;
        }

        /// <summary>
        /// Update request entity
        /// </summary>
        private async Task UpdateRequest(Request request, Func<Request, bool> makeRequest, IPrincipal user)
        {
            var isStatusChangedFromQuoted = request.RequestStatusId == 20;

            var isUpdatedRequest = makeRequest(request);

            var isStatusChangedFromQuotedToPurchase = isStatusChangedFromQuoted && request.RequestStatusId == 30;
            if(isStatusChangedFromQuotedToPurchase)
            {
                request.RequestStatusId = 999; // split request
            }
            if(isUpdatedRequest)
            {
                _logger.LogInformation("Updating request (id = {0}).", request.Id);

                if(user.IsManager())
                {
                    request.Manager = await _userManager.GetManagerByEmailAsync(user.Identity.Name);
                }

                _contextDb.Requests.Update(request);
                await _contextDb.SaveChangesAsync();
                _logger.LogInformation("Updated request (id = {0}).", request.Id);
            }
        }

        /// <summary>
        /// Update request entity
        /// </summary>
        private async Task UpdateDetailsRequest(Request request, Func<Request, Task<bool>> makeRequestDetailsLoader)
        {
            //updated request details
            _logger.LogInformation("Creating or Updating request details.");

            var isUpdatedRequestDetails = await makeRequestDetailsLoader(request);
            if(isUpdatedRequestDetails)
            {
                await _contextDb.SaveChangesAsync();
                _logger.LogInformation("Created or Updated request details.");
            }
        }

        #endregion
    }
}