using Domain.User.Request;

namespace Domain.User.Driver
{
    public interface IUserDriverPort
    {
        Task<IEnumerable<UserEntity>> CreateAsync(CreateUserRequest request, CancellationToken cancellation = default);

        Task<string> LoginUserAsync(string username, string password, CancellationToken cancellation = default);
    }
}
