using Domain.Session;

namespace RoomAsync.Test
{
    public class LoginUseCaseTests : BaseTest
    {
        public LoginUseCaseTests(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public async Task Login_ValidCredentials_ShouldReturnSessionId()
        {
            // Arrange
            var username = "Admin";
            var password = "mypassword";
            // Act
            var user = await LoginAsync(username, password, CancellationToken.None);

            // Assert
            Assert.NotNull(user.UserId);
        }
    }

    public class UserInfo : IUserSessionDataEntity
    {
        public string SessionId { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Roles { get; set; }
        public string? Claims { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
