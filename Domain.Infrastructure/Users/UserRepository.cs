using Domain.User;
using Domain.User.Driven;
using Domain.User.Request;
using Domain.User.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public UserRepository(ApplicationDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }

        public async Task AddUser(CreateUserRequest user, CancellationToken cancellation = default)
        {
            var userData = new UserDataEntity
            {
                Username = user.Username,
                PasswordHash = user.Password,
                UserId = Guid.NewGuid().ToString(),
            };
            _loggerService.LogInformation($"Adding user: {user.Username}");
            _dbContext.Users.Add(userData);
            await _dbContext.SaveChangesAsync(cancellation);
        }

        public async Task<IEnumerable<UserEntity>> Find(ISpecification<UserEntity> specification, CancellationToken cancellation = default)
        {
            if (specification is GetByNameSpec getByNameSpec)
            {
                var dbUser = await _dbContext.Users
                 .Where(u => u.Username == getByNameSpec.Username)
                    .ToListAsync(cancellation);

                if (dbUser == null)
                {
                    throw new ArgumentNullException("");
                }

                var result = dbUser.Select(x => new UserEntity(x));

                return result;
            }

            throw new NotSupportedException("Unsupported specification");
        }
    }
}
