using Domain.Reservation;

namespace Domain
{
    public interface ICodeGeneratorService
    {
        /// <summary>
        /// Generates a unique 6-character code for a reservation.
        /// </summary>
        /// <returns>A unique 6-character code.</returns>
        Task<ReservationCode> GenerateUniqueReservationCodeAsync(CancellationToken cancellationToken = default);
    }
}
