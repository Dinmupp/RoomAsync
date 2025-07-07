using Domain.Reservation;

namespace Domain.ReservationHolder.Request
{
    public class SelfCheckOutRequest
    {
        public Code Code { get; set; }
        public SelfCheckOutRequest(Code code)
        {
            Code = code;
        }
    }
}
