using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QFlow.Data.Managers;
using QFlow.Models.DataModels.Emails;
using Serilog;

namespace QFlow.Services
{
    public class EmailNotificationService : HostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailNotificationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var emailSenderService = _serviceProvider.GetRequiredService<EmailSenderService>();
            var settingsService = _serviceProvider.GetRequiredService<SettingsService>();

            var notificationManager = EmailNotificationManager.GetNewManager(settingsService);
            
            while(!cancellationToken.IsCancellationRequested)
            {
                var notifications = await notificationManager.GetNotSentNotificationAsync();
                var emailNotifications = notifications.ToList();
                if(emailNotifications.Any())
                {
                    foreach(var notification in emailNotifications)
                    {
                        await notificationManager.ProcessingNotificationAsync(notification);
                        var emailsTo = notification.EmailsTo.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach(var emailTo in emailsTo)
                        {
                            try 
                            {
                                await emailSenderService.SendEmailAsync(emailTo, notification.Subject, notification.Body);
                            }
                            catch (Exception e)
                            {
                               Log.Logger.Error(e ,e.Message);
                            }
                        }

                        await notificationManager.CompleatedNotificationAsync(notification);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }

    }
}
