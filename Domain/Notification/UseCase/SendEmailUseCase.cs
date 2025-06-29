using Domain.Error;
using Domain.Notification.Request;

namespace Domain.Notification.UseCase
{
    public class SendEmailUseCase
    {
        private readonly IEmailNotificationAdapter _emailAdapter;
        public SendEmailUseCase(IEmailNotificationAdapter emailAdapter)
        {
            _emailAdapter = emailAdapter;
        }
        public async Task<VoidResult<Response.Fail>> Execute(EmailRequest request)
        {
            try
            {
                await _emailAdapter.SendEmailAsync(request);
            }
            catch (Exception e)
            {
                return new Response.Fail.SendEmailError(e.Message);
            }

            return new Success();
        }
        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class SendEmailError(string message) : Fail(message);
            }
            public class Success
            {
                public Success(string messageId) => MessageId = messageId;
                public string MessageId { get; set; }
            }
        }
    }
}
