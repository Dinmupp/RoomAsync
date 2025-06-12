using Domain;
using Domain.User.UseCases;

namespace RoomAsync.Test.User
{
    public class CreateUserTest : BaseTest
    {
        private readonly ILoggerService _loggerService;

        public CreateUserTest(TestFixture testFixture) : base(testFixture)
        {
            _loggerService = Scope.ServiceProvider.GetRequiredService<ILoggerService>();
        }

        [Fact]
        public async Task CreateUserShould()
        {
            var username = "Admin";
            var password = "mypassword";

            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");
            // Act
            var user = await LoginAsync(username, password, CancellationToken.None);

            var result = await UserDriver.CreateAsync(new Domain.User.Request.CreateUserRequest
            {
                Username = "testuser",
                Password = "password",
            }, CancellationToken.None);

            if (result.TryGetValue(out var dbUser))
            {
                Assert.True(dbUser.Users.First().GetSafe().Length > 0);
            }
        }

        [Fact]
        public async Task CreateUserWithoutPermissionShould()
        {
            var username = "UnvalidUser";
            var password = "mypassword";

            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");
            // Act
            var user = await LoginAsync(username, password, CancellationToken.None);

            var response = await UserDriver.CreateAsync(new Domain.User.Request.CreateUserRequest
            {
                Username = "testuser",
                Password = "password",
            }, CancellationToken.None);

            if (response.TryGetError(out var error))
            {
                Assert.True(error is CreateUserUseCase.Response.Fail.UnauthorizedAccess);
            }
        }
    }
}
