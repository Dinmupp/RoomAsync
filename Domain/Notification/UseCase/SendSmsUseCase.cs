using Domain.Error;
using Domain.Notification.Request;

namespace Domain.Notification.UseCase
{
    public class SendSmsUseCase
    {
        private readonly ISmsNotificationAdapter _smsAdapter;
        public SendSmsUseCase(ISmsNotificationAdapter smsAdapter)
        {
            _smsAdapter = smsAdapter;
        }
        public async Task<VoidResult<Response.Fail>> Execute(SmsRequest request)
        {
            try
            {
                await _smsAdapter.SendSmsAsync(request);
            }
            catch (Exception e)
            {
                return new Response.Fail.SendSmsError(e.Message);
            }

            return new Success();
        }
        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class SendSmsError(string message) : Fail(message);
            }
            public class Success
            {
                public Success(string messageId) => MessageId = messageId;
                public string MessageId { get; set; }
            }
        }
    }
}
