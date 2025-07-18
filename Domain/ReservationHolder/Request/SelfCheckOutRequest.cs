using Domain.Reservation;

namespace Domain.ReservationHolder.Request
{
    public class SelfCheckOutRequest
    {
        public ReservationCode Code { get; set; }
        public SelfCheckOutRequest(ReservationCode code)
        {
            Code = code;
        }
    }
}
