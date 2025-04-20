using Domain;
using Domain.User.Driver;

namespace RoomAsync.Test
{
    public class CreateUserTest : IAsyncDisposable
    {
        readonly AsyncServiceScope _scope;
        private readonly IUserDriverPort _userAdapter; //DRIVERORT
        private readonly ILoggerService _loggerService;

        public CreateUserTest(TestFixture fixture)
        {
            _scope = fixture.CreateScopeAsync();
            _userAdapter = _scope.ServiceProvider.GetRequiredService<IUserDriverPort>();
            _loggerService = _scope.ServiceProvider.GetRequiredService<ILoggerService>();
        }

        [Fact]
        public async Task CreateUserShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");
            var test = await _userAdapter.CreateAsync(new Domain.User.Request.CreateUserRequest
            {
                Username = "testuser",
                Password = "password",
            }, CancellationToken.None);
            Assert.True(test.First().Username == "testuser");
        }

        public ValueTask DisposeAsync() => _scope.DisposeAsync();
    }
}
