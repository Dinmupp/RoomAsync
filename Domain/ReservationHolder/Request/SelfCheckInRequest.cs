using Domain.Reservation;

namespace Domain.ReservationHolder.Request
{
    public class SelfCheckInRequest
    {
        public Code Code { get; set; }
        public SelfCheckInRequest(Code code)
        {
            Code = code;
        }
    }
}
