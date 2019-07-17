using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class EmailSender : IEmailSender
    {
        private const string FROM_EMAIL = "no-reply@marketplace.com";
        private const string SENDER_NAME = "Marketplace";
        private const string RECEIVER_USER = "Marketplace User";
        private string ApiKey;
        public EmailSender(IConfiguration Configuration)
        {
            this.ApiKey = Configuration["MarketplaceSendGridKey"];
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(this.ApiKey, subject, email, message);
        }

        private async Task Execute(string apiKey, string subjectInput, string email, string message)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FROM_EMAIL, SENDER_NAME);
            var subject = subjectInput;
            var to = new EmailAddress(email, RECEIVER_USER);
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
