using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BusinessApp.Core.ApplicationService.ServiceSetting;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BusinessApp.Core.ApplicationService.Service
{
    public class EmailService : IEmailService
    {
        private readonly SendGridOptions _options;

        public EmailService(IOptions<SendGridOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(_options.SendGridKey, subject, message, email);
        }


        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("My@App.com", "Baba Dev"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }
    }
}
