using Domain.ContactWay;

namespace Domain.Notification.Request
{
    public class EmailRequest
    {
        public EmailRequest(Email email, string subject, string body)
        {
            Email = email;
            Subject = subject;
            Body = body;
        }

        public Email Email { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
