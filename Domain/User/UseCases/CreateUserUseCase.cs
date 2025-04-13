using Domain.User.Driven;
using Domain.User.Request;
using Domain.User.Specifications;

namespace Domain.User.UseCases
{
    public class CreateUserUseCase // DOMAINSERVICE
    {
        private readonly IUserRepository _userRepository; //DRIVEN

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserEntity>> Execute(CreateUserRequest request, CancellationToken cancellation = default)
        {
            await _userRepository.AddUser(request, cancellation);

            var user = await _userRepository.Find(new GetByNameSpec(request.Username), cancellation);
            return user;
        }
    }
}
