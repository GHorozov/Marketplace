using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;


namespace Marketplace.Services
{
    public class EmailSender : IEmailSender
    {
        private const string FROM_EMAIL = "no-reply@marketplace.com";
        private const string SENDER_NAME = "Marketplace";
        private string ApiKey;
        public EmailSender(IConfiguration Configuration)
        {
            this.ApiKey = Configuration["Authentication:SendGridKey"];
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(this.ApiKey, subject, email, message);
        }

        private Task Execute(string apiKey, string subjectInput, string email, string message)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FROM_EMAIL, SENDER_NAME);
            var subject = subjectInput;
            var to = new EmailAddress(email, "User");
            var plainTextContent = message;
            var htmlContent = message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            return client.SendEmailAsync(msg);
        }
    }
}
