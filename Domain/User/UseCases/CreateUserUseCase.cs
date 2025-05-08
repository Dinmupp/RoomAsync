using Domain.Error;
using Domain.User.Driven;
using Domain.User.Request;
using Domain.User.Specifications;

namespace Domain.User.UseCases
{
    public sealed class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserContext _userContext;

        public CreateUserUseCase(IUserRepository userRepository, UserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(CreateUserRequest request, CancellationToken cancellation = default)
        {
            if (_userContext.Roles.Contains("Admin") || _userContext.Roles.Contains("System Admin"))
            {
                await _userRepository.AddUser(request, cancellation);

                var user = await _userRepository.Find(new GetByNameSpec(request.Username), cancellation);
                return new Response.Success(user.Select(x => x.UserId));
            }

            return new Response.Fail.UnauthorizedAccess();
        }

        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class UnauthorizedAccess() : Fail(string.Empty);
            }

            public class Success
            {
                public Success(IEnumerable<UserId> users) => Users = users;
                public IEnumerable<UserId> Users { get; set; }
            }
        }
    }
}
