using Domain.Error;
using Domain.User.Driven;
using Domain.User.Request;
using Domain.User.Specifications;

namespace Domain.User.UseCases
{
    public sealed class CreateUserUseCase // DOMAINSERVICE
    {
        private readonly IUserRepository _userRepository; //DRIVEN
        private readonly UserContext _userContext; //DRIVEN

        public CreateUserUseCase(IUserRepository userRepository, UserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<Result<CreateUserUseCase.Response.Success, CreateUserUseCase.Response.Fail>> Execute(CreateUserRequest request, CancellationToken cancellation = default)
        {
            if (!_userContext.Roles.Contains("Admin") || !_userContext.Roles.Contains("System Admin"))
            {
                return new CreateUserUseCase.Response.Fail.UnauthorizedAccess();
            }

            await _userRepository.AddUser(request, cancellation);

            var user = await _userRepository.Find(new GetByNameSpec(request.Username), cancellation);
            return new CreateUserUseCase.Response.Success(user);
        }

        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class UnauthorizedAccess() : Fail(string.Empty);
            }

            public class Success
            {
                public Success(IEnumerable<UserEntity> users) => Users = users;
                public IEnumerable<UserEntity> Users { get; set; }
            }
        }
    }
}
