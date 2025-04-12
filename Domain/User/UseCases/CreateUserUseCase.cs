using Domain.User.Driven;

namespace Domain.User.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(string username, string passwordHash, string role)
        {
            var user = new Request.CreateUserRequest
            {
                Username = username,
                Password = passwordHash
            };

            await _userRepository.AddUser(user);
        }
    }
}
