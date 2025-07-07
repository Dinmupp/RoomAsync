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

        public async Task<string> GenerateUniqueReservationCodeAsync(CancellationToken cancellationToken = default)
        {
            string code;
            do
            {
                code = new string(Enumerable.Repeat(Chars, 6)
                    .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());

                // Ensure code is unique among active reservations (not checked out)
            } while (await _dbContext.Reservations
                .AnyAsync(r => r.Code == code && r.CheckedOutAt == null, cancellationToken));

            return code;
        }
    }
}
