using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QFlow.Models.DataModels.Alerts;

namespace QFlow.Data.Managers
{
    public class AlertNotificationManager
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _contextDb;

        public AlertNotificationManager(ApplicationDbContext context, ILogger<AlertNotificationManager> logger)
        {
            _contextDb = context;
            _logger = logger;
        }

        /// <summary>
        ///  Get alerts by page
        /// </summary>
        public async Task<IEnumerable<AlertNotification>> GetAlertsNotificationByEmailAsync(string email, int takeCount)
        {
            _logger.LogInformation($"Get alerts take (count = {takeCount}).");
            var dateNow = DateTime.UtcNow;
            return await _contextDb.AlertNotifications
                                    .Where(_ => _.Email == email && _.Date <= dateNow)
                                    .OrderByDescending(_ => _.Date)
                                    .Take(takeCount)
                                        .ToListAsync();
        }

        /// <summary>
        ///  Get Get alerts
        /// </summary>
        public async Task<IEnumerable<AlertNotification>> GetAlertsNotificationPageAsync(string email, int startIndex = 0, int pageCount = 10)
        {
            _logger.LogInformation($"Get alerts page (startIndex = {startIndex} , pageCount={pageCount}).");
            var dateNow = DateTime.UtcNow;
            return await _contextDb.AlertNotifications
                            .Where(_ => _.Email == email && _.Date <= dateNow)
                            .OrderByDescending(_ => _.Date)
                            .Skip(startIndex)
                            .Take(pageCount)
                                .ToListAsync();
        }

        /// <summary>
        ///  Get Get alerts
        /// </summary>
        public async Task<bool> HasAlertsByEmail(string email)
        {
            var dateNow = DateTime.UtcNow;
            var alert = await _contextDb.AlertNotifications
                                        .FirstOrDefaultAsync(_ =>
                                                            _.Email == email
                                                            && !_.IsRead
                                                            && _.Date < dateNow);
            return alert != null;
        }

        /// <summary>
        ///  Create Notification
        /// </summary>
        public async Task CreateNotificationAlertAsync(string text, string email)
        {
            await CreateNotificationAlertAsync(text, email, DateTime.UtcNow, null, null);
        }

        /// <summary>
        ///  Create Notification
        /// </summary>
        public async Task CreateNotificationAlertAsync(string text, string email, DateTime date, int? requestId, int? alertTypeId)
        {
            _logger.LogInformation($"Create notificationAlert. (user='{email})'");

            var newAlertNotification = new AlertNotification
            {
                Id = 0,
                Text = text,
                Email = email,
                Date = date,
                IsRead = false,
                RequestId = requestId,
                AlertTypeId = alertTypeId
            };

            await _contextDb.AlertNotifications.AddAsync(newAlertNotification);
            await _contextDb.SaveChangesAsync();

            _logger.LogInformation($"Created notification. (id={newAlertNotification.Id})'");
        }

        /// <summary>
        ///  Create Notification
        /// </summary>
        public async Task RemoveByRequestIdAndAlertTypeAsync(int requestId, int alertTypeId)
        {
            var alerts = await _contextDb.AlertNotifications
                                            .Where(_ => _.RequestId == requestId && _.AlertTypeId == alertTypeId && !_.IsRead)
                                            .ToListAsync();
            if(alerts.Any())
            {
                _contextDb.AlertNotifications.RemoveRange(alerts);
                await _contextDb.SaveChangesAsync();
            }
        }

        /// <summary>
        ///  Set IsRead
        /// </summary>
        public async Task SetIsReadAlertNotificationAsync(string email)
        {
            var alerts = await _contextDb.AlertNotifications.Where(_ => _.Email == email && !_.IsRead).ToListAsync();
            foreach(var alert in alerts)
            {
                alert.IsRead = true;
                _contextDb.AlertNotifications.Update(alert);
            }

            await _contextDb.SaveChangesAsync();
        }

        /// <summary>
        ///  Get AlertNotifications Count
        /// </summary>
        public async Task<int> GetAlertCountAsync(string email)
        {
            _logger.LogInformation("Get AlertNotifications count");

            var dateNow = DateTime.UtcNow;
            return await _contextDb.AlertNotifications.Where(_ => _.Email == email && _.Date <= dateNow).CountAsync();
        }

        /// <summary>
        ///  updated email
        /// </summary>
        public async Task UpdateEmail(string oldEmail, string newEmail)
        {
            var dateNow = DateTime.UtcNow;
            var alerts = await _contextDb.AlertNotifications
                                            .Where(_ => _.Email == oldEmail && !_.IsRead && _.Date <= dateNow)
                                            .ToListAsync();
            foreach(var alert in alerts)
            {
                alert.Email = newEmail;
                _contextDb.AlertNotifications.Update(alert);
            }

            await _contextDb.SaveChangesAsync();
        }

    }
}
