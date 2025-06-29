using Domain.Notification;
using Domain.Notification.Request;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TwilioAdapter
{
    public class TwilioEmailProvider : IEmailNotificationAdapter
    {
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _application;

        public TwilioEmailProvider(string apiKey, string fromEmail, string application)
        {
            _apiKey = apiKey;
            _fromEmail = fromEmail;
            _application = application;
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _application);
            var toEmail = new EmailAddress(request.Email.Value);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, request.Subject, request.Body, request.Body);
            await client.SendEmailAsync(msg);
        }
    }
}
