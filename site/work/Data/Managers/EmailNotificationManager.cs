using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QFlow.Models.DataModels.Emails;
using QFlow.Services;

namespace QFlow.Data.Managers
{
    public class EmailNotificationManager
    {
        private readonly ApplicationDbContext _contextDb;

        public EmailNotificationManager(ApplicationDbContext context)
        {
            _contextDb = context;
        }

        /// <summary>
        ///  Get didnt send notifications
        /// </summary>
        public async Task<IEnumerable<EmailNotification>> GetNotSentNotificationAsync()
        {
            return await _contextDb.EmailNotifications
                                    .Where(_ => !_.IsProcessing
                                                && !_.IsSent
                                                && (_.SendDate < DateTime.UtcNow || _.SendDate == null))
                                    .ToListAsync();
        }

        /// <summary>
        ///  Create EmailNotification
        /// </summary>
        public async Task CreateNotificationAsync(string emailTo, string subject, string body, string creatorEmail = null)
        {
            var emailsTo = new[] {emailTo};
            await CreateNotificationAsync(emailsTo, subject, body, creatorEmail, null, null, null);
        }

        /// <summary>
        ///  Create EmailNotification
        /// </summary>
        public async Task CreateNotificationAsync(string[] emailsTo, string subject, string body, string creatorEmail = null)
        {
            await CreateNotificationAsync(emailsTo, subject, body, creatorEmail, null, null, null);
        }

        /// <summary>
        ///  Create Notification
        /// </summary>
        public async Task RemoveByRequestIdAndAlertTypeAsync(int requestId, int alertTypeId)
        {
            var emailNotifications = await _contextDb.EmailNotifications
                                            .Where(_ => _.RequestId == requestId && _.AlertTypeId == alertTypeId && !_.IsSent)
                                            .ToListAsync();
            if(emailNotifications.Any())
            {
                _contextDb.EmailNotifications.RemoveRange(emailNotifications);
                await _contextDb.SaveChangesAsync();
            }
        }

        /// <summary>
        ///  Create EmailNotification
        /// </summary>
        public async Task CreateNotificationAsync(string[] emailsTo, string subject, string body, string creatorEmail, DateTime? sendDate, int? requestId, int? alertTypeId)
        {
            var eamilsToStr = string.Join(",", emailsTo);

            var newNotification = new EmailNotification
            {
                Id = 0,
                Body = body,
                Subject = subject,
                EmailsTo = eamilsToStr,
                IsProcessing = false,
                IsSent = false,
                SendDate = sendDate,

                CreatedBy = creatorEmail,
                Created = DateTime.UtcNow,

                AlertTypeId = alertTypeId,
                RequestId = requestId
            };

            await _contextDb.EmailNotifications.AddAsync(newNotification);
            await _contextDb.SaveChangesAsync();
        }

        /// <summary>
        ///  Set IsProcessing EmailNotification
        /// </summary>
        public async Task ProcessingNotificationAsync(EmailNotification emailNotification)
        {
            emailNotification.IsProcessing = true;
            emailNotification.IsSent = false;

            _contextDb.EmailNotifications.Update(emailNotification);
            await _contextDb.SaveChangesAsync();
        }

        /// <summary>
        ///  Set IsSent EmailNotification
        /// </summary>
        public async Task CompleatedNotificationAsync(EmailNotification emailNotification)
        {
            emailNotification.IsProcessing = false;
            emailNotification.IsSent = true;
            emailNotification.Completed = DateTime.UtcNow;

            _contextDb.EmailNotifications.Update(emailNotification);
            await _contextDb.SaveChangesAsync();
        }

        public static EmailNotificationManager GetNewManager(SettingsService settingsService)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(settingsService.ConnectionString);
            var context = new ApplicationDbContext(optionsBuilder.Options);

            return new EmailNotificationManager(context);
        }
    }
}
