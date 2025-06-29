using Domain.Notification.Request;

namespace Domain.Notification
{
    public interface IEmailNotificationAdapter
    {
        Task SendEmailAsync(EmailRequest request);
    }
}
