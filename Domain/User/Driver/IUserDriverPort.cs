using Domain.User.Request;

namespace Domain.User.Driver
{
    public interface IUserDriverPort
    {
        Task<IEnumerable<UserEntity>> CreateAsync(CreateUserRequest request, CancellationToken cancellation = default);
    }
}
