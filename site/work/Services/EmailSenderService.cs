using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace QFlow.Services
{
    public class EmailSenderService
    {
        private readonly SettingsService _settings;

        public EmailSenderService( SettingsService settings )
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(_settings.SendGridKey))
            {
                throw new NullReferenceException("Send grid key can't be null or empty.");
            }
            await SendAsync(_settings.SendGridKey, subject, message, email );
        }

        private async Task SendAsync( string apiKey, string subject, string message, string email )
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_settings?.EmailFrom ?? "no-reply@qflow.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo( new EmailAddress( email ) );
            await client.SendEmailAsync( msg );
        }
    }
}
