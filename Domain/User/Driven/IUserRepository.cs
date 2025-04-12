using Domain.User.Request;

namespace Domain.User.Driven
{
    public interface IUserRepository
    {
        Task AddUser(CreateUserRequest user);
        Task<UserEntity> Find(ISpecification<UserEntity> specification);
    }
}
