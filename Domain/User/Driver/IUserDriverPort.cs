using Domain.User.Request;
using Domain.User.UseCases;

namespace Domain.User.Driver
{
    public interface IUserDriverPort
    {
        Task<Result<CreateUserUseCase.Response.Success, CreateUserUseCase.Response.Fail>> CreateAsync(CreateUserRequest request, CancellationToken cancellation = default);

        Task<UserContext> LoginUserAsync(string username, string password, CancellationToken cancellation = default);
    }
}
