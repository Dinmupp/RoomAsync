using Domain.User;
using Domain.User.Driver;
using Domain.User.Request;
using Domain.User.UseCases;

namespace Application.User
{
    public class UserDriverImplementation : IUserDriverPort
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly LoginUseCase _loginUseCase;
        public UserDriverImplementation(CreateUserUseCase createUserUseCase, LoginUseCase loginUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _loginUseCase = loginUseCase;
        }

        public async Task<IEnumerable<UserEntity>> CreateAsync(CreateUserRequest request, CancellationToken cancellation = default) =>
           await _createUserUseCase.Execute(request, cancellation);

        public Task<string> LoginUserAsync(string username, string password, CancellationToken cancellation = default) =>
            _loginUseCase.ExecuteAsync(username, password, cancellation);
    }
}
