using Domain.User.Request;

namespace Domain.User.Driven
{
    public interface IUserRepository
    {
        Task AddUser(CreateUserRequest user, CancellationToken cancellation = default);
        Task<IEnumerable<UserEntity>> Find(ISpecification<UserEntity> specification, CancellationToken cancellation = default);
    }
}
