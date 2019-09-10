using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BusinessApp.Core.ApplicationService.ServiceSetting;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BusinessApp.Core.ApplicationService.Service
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailService : IEmailService
    {
        public EmailSenderOptions Options { get; }

        public EmailService(IOptions<EmailSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            string SendGridKey = "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";
            
            return Execute(SendGridKey, subject, message, email);
            //throw new System.NotImplementedException();
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

        static async Task Execute2(string email)
        {
            var apiKey = Environment.GetEnvironmentVariable("chris");
            var apiKey1 = Environment.GetEnvironmentVariable("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");

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
