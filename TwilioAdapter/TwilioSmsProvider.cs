using Domain.Notification;
using Domain.Notification.Request;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TwilioAdapter
{
    public class TwilioSmsProvider : ISmsNotificationAdapter
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;
        public TwilioSmsProvider(string accountSid, string authToken, string fromNumber)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _fromNumber = fromNumber;
        }
        public async Task SendSmsAsync(SmsRequest request)
        {
            try
            {
                TwilioClient.Init(_accountSid, _authToken);
                await MessageResource.CreateAsync(
                    to: new PhoneNumber(request.PhoneNumber.ToString()),
                    from: new PhoneNumber(_fromNumber),
                    body: request.Message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Failed to send SMS", ex);
            }

        }
    }
}
