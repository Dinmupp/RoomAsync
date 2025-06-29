using Domain.Notification.Request;

namespace Domain.Notification
{
    public interface ISmsNotificationAdapter
    {
        Task SendSmsAsync(SmsRequest request);
    }
}
