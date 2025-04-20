using Domain.User;
using Domain.User.Driver;
using Domain.User.Request;
using Domain.User.UseCases;

namespace Application.User
{
    public class UserDriverImplementation : IUserDriverPort
    {
        private readonly CreateUserUseCase _createUserUseCase;
        public UserDriverImplementation(CreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        public async Task<IEnumerable<UserEntity>> CreateAsync(CreateUserRequest request, CancellationToken cancellation = default) =>
           await _createUserUseCase.Execute(request, cancellation);
    }
}
