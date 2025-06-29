using Domain.ContactWay;

namespace Domain.Notification.Request
{
    public class SmsRequest
    {
        public SmsRequest(Phone phoneNumber, string message)
        {
            PhoneNumber = phoneNumber;
            Message = message;
        }
        public Phone PhoneNumber { get; }
        public string Message { get; }
    }
}
