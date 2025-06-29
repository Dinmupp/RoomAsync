using Domain.Notification.Request;
using Domain.Notification.UseCase;

namespace Domain.Notification.Driver
{
    public class NotificationDriverImplementation : INotificationDriverPort
    {
        private readonly SendSmsUseCase _sendSmsUseCase;
        private readonly SendEmailUseCase _sendEmailUseCase;
        public NotificationDriverImplementation(SendSmsUseCase sendSmsUseCase, SendEmailUseCase sendEmailUseCase)
        {
            _sendSmsUseCase = sendSmsUseCase;
            _sendEmailUseCase = sendEmailUseCase;
        }

        public async Task<VoidResult<SendSmsUseCase.Response.Fail>> SendSms(SmsRequest request) =>
            await _sendSmsUseCase.Execute(request);

        public async Task<VoidResult<SendEmailUseCase.Response.Fail>> SendEmail(EmailRequest request) =>
                await _sendEmailUseCase.Execute(request);
    }
}
