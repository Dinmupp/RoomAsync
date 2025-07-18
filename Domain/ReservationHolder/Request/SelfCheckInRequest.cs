using Domain.Reservation;

namespace Domain.ReservationHolder.Request
{
    public class SelfCheckInRequest
    {
        public ReservationCode Code { get; set; }
        public SelfCheckInRequest(ReservationCode code)
        {
            Code = code;
        }
    }
}
