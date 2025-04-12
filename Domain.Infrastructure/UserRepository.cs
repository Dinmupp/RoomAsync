using Domain.User;
using Domain.User.Driven;
using Domain.User.Request;
using Domain.User.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(CreateUserRequest user)
        {
            var userData = new UserDataEntity
            {
                Username = user.Username,
                PasswordHash = user.Password
            };
            _dbContext.Users.Add(userData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserEntity> Find(ISpecification<UserEntity> specification)
        {
            if (specification is GetByNameSpec getByNameSpec)
            {
                var dbUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => getByNameSpec.IsSatisfiedBy(new UserEntity(u)));

                if (dbUser == null)
                {
                    throw new ArgumentNullException("");
                }

                return new UserEntity(dbUser);
            }

            throw new NotSupportedException("Unsupported specification");
        }
    }
}
