using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QFlow.Data.Managers;
using QFlow.Models.DataModels;
using QFlow.Models.DataModels.Alerts;
using QFlow.Models.DataModels.Requests;
using QFlow.Models.DataModels.Users;

namespace QFlow.Helper
{
    public class NotificationHelper
    {
        private readonly MessageManager _messageManager;
        private readonly EmailNotificationManager _emailNotificationManager;
        private readonly AlertNotificationManager _alertNotificationManager;
        private readonly UserManager _userManager;
        private readonly AlertTypeManager _alertTypeManager;
        private readonly RequestManager _requestManager;

        public NotificationHelper(
            MessageManager messageManager,
            EmailNotificationManager emailNotificationManager,
            UserManager userManager,
            AlertNotificationManager alertNotificationManager,
            RequestManager requestManager,
            AlertTypeManager alertTypeManager)
        {
            _messageManager = messageManager;
            _emailNotificationManager = emailNotificationManager;
            _userManager = userManager;
            _alertNotificationManager = alertNotificationManager;
            _alertTypeManager = alertTypeManager;
            _requestManager = requestManager;
        }

        /// <summary>
        /// Processing EmailNotification after creted or updated request
        /// </summary>
        public async Task ProcessingNotificationAndAlert(Request request, string linkToRequest, bool isReturnedStatus)
        {
            await CreateEmailNotification(request, linkToRequest, isReturnedStatus);
            await CreateAlertNotificationByUpdateStatus(request, linkToRequest, isReturnedStatus);
            await UpdateAlertsByRequest(request, linkToRequest);
        }

        public async Task CreateEmailNotification(Request request, string linkToRequest, bool isReturnedStatus)
        {
            var isSendToClient = request.RequestStatusId == 20 || request.RequestStatusId == 40;
            var link = $"<a href='{linkToRequest}'>Link</a>";
            User manager = request.ManagerId.HasValue
                ? await _userManager.GetManagerAsync(request.ManagerId.Value)
                : null;

            var client = await _userManager.GetClientAsync(request.ClientId);
            if (isReturnedStatus)
            {
                var status = await _requestManager.GetStatusByIdAsync(request.RequestStatusId);
                var subject = "Returned stutus of request";
                var body = $"<b>{manager}</b> returned status of request(<b>{link}</b>) to <b>{status.Name.ToUpper()}</b>";

                await _emailNotificationManager.CreateNotificationAsync(new []{ client.Email }, "QFLOW : " + subject, body);
                return;
            }
            var messages = await _messageManager.GetMessagesByStatusAsync(request.RequestStatusId);
            foreach(var message in messages)
            {
                var emailsTo = new List<string>();
                if(isSendToClient)
                {
                    emailsTo.Add(client.Email);
                }
                else if(manager == null) // new request, sending to all managers
                {
                    var managers = await _userManager.GetAllManagers();
                    emailsTo.AddRange(managers.Select(_ => _.Email));
                }
                else
                {
                    emailsTo.Add(manager.Email);
                }
                var subject = message.Subject;
                var body = message.Text
                    .Replace("[CLIENT]", client.ToString())
                    .Replace("[MANAGER]", manager?.ToString())
                    .Replace("[LINK]", link);

                await _emailNotificationManager.CreateNotificationAsync(emailsTo.ToArray(), "QFLOW : " + subject, body);
            }
        }

        /// <summary>
        /// Add Alert
        /// </summary>
        public async Task CreateAlertNotificationByUpdateStatus(Request request, string linkToRequest, bool isReturnedStatus)
        {
            var link = $"<a href='{linkToRequest}' target='_blank'>Link</a>";
            var client = await _userManager.GetClientAsync(request.ClientId);
            if(isReturnedStatus)
            {
                User manager = request.ManagerId.HasValue
                                ? await _userManager.GetManagerAsync(request.ManagerId.Value)
                                : null;
                if (manager == null)
                    return;

                var status = await _requestManager.GetStatusByIdAsync(request.RequestStatusId);
                var body = $"<b>{manager}</b><br>returned status of request(<b>{link}</b>) to <b>{status.Name.ToUpper()}</b>";

                await _alertNotificationManager.CreateNotificationAlertAsync(body, client.Email);
                return;
            }
            var isNewRequest = request.RequestStatusId == 10;
            if(isNewRequest)
            {
                var text = $"<b>{client}</b><br/>created new request - <b>{link}</b>";
                var managers = await _userManager.GetAllManagers();
                foreach(var manager in managers)
                {
                    await _alertNotificationManager.CreateNotificationAlertAsync(text, manager.Email);
                }
                return;
            }

            var isClientChangeRequest = request.RequestStatusId == 20 || request.RequestStatusId == 40;
            var managerRequest = await _userManager.GetManagerAsync(request.ManagerId ?? 0);

            var userEmailTo = isClientChangeRequest ? client.Email : managerRequest?.Email;
            var userCreator = isClientChangeRequest ? managerRequest?.ToString() : client.ToString();

            var hasEmails = !string.IsNullOrEmpty(userEmailTo) && !string.IsNullOrEmpty(userCreator);
            if(hasEmails)
            {
                var text = $"<b>{userCreator}</b><br/>updated status of request (<b>{link}</b>) to <b>{request.RequestStatus?.Name.ToUpper()}</b>.";
                await _alertNotificationManager.CreateNotificationAlertAsync(text, userEmailTo);
            }
        }

        /// <summary>
        /// Add Alert
        /// </summary>
        public async Task UpdateAlertsByRequest(Request request, string linkToRequest)
        {
            var managerRequest = await _userManager.GetManagerAsync(request.ManagerId ?? 0);
            if(managerRequest == null)
            {
                return;
            }

            var allAlertTypes = await _alertTypeManager.GetAllAlertTypes();
            var link = $"<a href='{linkToRequest}' target='_blank'>Link</a>";
            foreach(var detail in request.Details)
                foreach(var alertType in allAlertTypes)
                {
                    async Task UpdateAlert(DateTime? date)
                    {
                        if(!date.HasValue)
                            return;
                        await UpdatedAlert(
                                            alertType.Id, 
                                            alertType.Message, 
                                            alertType.Name, 
                                            alertType.IsSendingEmail, 
                                            request.Id, 
                                            managerRequest.Email, 
                                            date.Value, link);
                    }

                    switch(alertType.Name)
                    {
                        case AlertType.SupplierPaymentDue:
                            await UpdateAlert(detail.PaymentDueDate);
                            break;
                        case AlertType.ConfirmedDeliveryDate:
                            await UpdateAlert(detail.ConfirmedDeliveryDate);
                            break;
                        case AlertType.ServiceDue:
                            await UpdateAlert(detail.ServiceDueDate);
                            break;
                        case AlertType.DeInstallDue:
                            await UpdateAlert(detail.DeInstallDueDate);
                            break;
                    }
                }
        }

        public async Task CreateAlertsByEstimateDeliveryDate(Request request, string linkToRequest)
        {
            foreach (var detail in request.Details)
            {
                if (!detail.EstimateDeliveryDate.HasValue)
                    continue;

                var client = await _userManager.GetClientAsync(request.ClientId);
                if (client == null)
                    return;

                var manager = await _userManager.GetManagerAsync(request.ManagerId ?? 0);
                if (manager == null)
                    return;

                var text =
                    $"<b>{manager}</b><br/>updated ESTIMATE DELIVERY DATE of request (<b><a href='{linkToRequest}' target='_blank'>Link</a></b>) to <b>{detail.EstimateDeliveryDate.Value:yyyy-MM-dd}</b>.";

                await _emailNotificationManager.CreateNotificationAsync(client.Email, "QFLOW : Updated ESTIMATE DELIVERYT DATE", text);
                await _alertNotificationManager.CreateNotificationAlertAsync(text, client.Email);
            }
        }

        #region Implementation routines

        private async Task UpdatedAlert(int alertId,
                                        string alertMessage,
                                        string alertName,
                                        bool alertIsSendingEmail,
                                        int requestId,
                                        string emailManger,
                                        DateTime date,
                                        string link)
        {
            //remove all alerts and emails by request and alerttype
            await _alertNotificationManager.RemoveByRequestIdAndAlertTypeAsync(requestId, alertId);
            await _emailNotificationManager.RemoveByRequestIdAndAlertTypeAsync(requestId, alertId);

            var periods = await _alertTypeManager.GetAlertTypePeriodTimesByAlertType(alertId);
            foreach(var period in periods)
            {
                var dateAlert = period.PeriodTime.GetDateTimeFrom(date);
                if(dateAlert < DateTime.UtcNow)
                {
                    continue;
                }

                var text = $"<b>{alertMessage} - {period.PeriodTime.Name}.</b><br/>Request - <b>{link}</b>";
                await _alertNotificationManager.CreateNotificationAlertAsync(text, emailManger, dateAlert, requestId, alertId);

                if(alertIsSendingEmail)
                {
                    var subject = $"QFLOW : {alertName}";
                    await _emailNotificationManager.CreateNotificationAsync(new[] { emailManger }, subject, text, null, dateAlert, requestId, alertId);
                }
            }
        }
        #endregion
    }
}
