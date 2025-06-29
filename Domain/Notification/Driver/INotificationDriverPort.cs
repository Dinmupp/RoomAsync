using Domain.Notification.Request;
using Domain.Notification.UseCase;

namespace Domain.Notification.Driver
{
    public interface INotificationDriverPort
    {
        Task<VoidResult<SendSmsUseCase.Response.Fail>> SendSms(SmsRequest request);
        Task<VoidResult<SendEmailUseCase.Response.Fail>> SendEmail(EmailRequest request);
    }
}
