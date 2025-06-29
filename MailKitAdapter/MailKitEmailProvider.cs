using Domain.Notification;
using Domain.Notification.Request;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailKitAdapter
{
    public class MailKitEmailProvider : IEmailNotificationAdapter
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _fromEmail;

        public MailKitEmailProvider(string host, int port, string username, string password, string fromEmail)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _fromEmail = fromEmail;
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_fromEmail, _fromEmail));
                message.To.Add(MailboxAddress.Parse(request.Email.Value));
                message.Subject = request.Subject;
                message.Body = new TextPart("plain") { Text = request.Body };

                using var client = new SmtpClient();
                await client.ConnectAsync(_host, _port, false);
                await client.AuthenticateAsync(_username, _password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Failed to send Email", ex);
            }
        }
    }
}
