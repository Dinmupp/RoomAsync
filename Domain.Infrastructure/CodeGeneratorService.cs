using Domain.Reservation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Domain.Infrastructure
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly ApplicationDbContext _dbContext;
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public CodeGeneratorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReservationCode> GenerateUniqueReservationCodeAsync(CancellationToken cancellationToken = default)
        {
            // Hämta alla aktiva koder från databasen
            var usedCodes = await _dbContext.Reservations
                .Where(r => r.CheckedOutAt == null)
                .Select(r => r.Code)
                .ToListAsync(cancellationToken);

            var usedSet = new HashSet<string>(usedCodes);

            const int maxAttempts = 100;
            Span<char> buffer = stackalloc char[6];

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                for (int i = 0; i < 6; i++)
                {
                    buffer[i] = Chars[RandomNumberGenerator.GetInt32(Chars.Length)];
                }
                var code = new string(buffer);

                if (!usedSet.Contains(code))
                    return new ReservationCode(code);
            }

            throw new InvalidOperationException("Could not generate a unique reservation code after multiple attempts.");
        }
    }
}
