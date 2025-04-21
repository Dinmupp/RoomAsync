namespace RoomAsync.Web.Tests;

public class WebTests
{
    // IGNORE FOR NOW THIS IS JUST A PLACEHOLDER FOR THE TESTS
    //[Fact]
    //public async Task GetWebResourceRootReturnsOkStatusCode()
    //{
    //    // Arrange
    //    var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.RoomAsync_Web_AppHost>(CancellationToken.None);
    //    appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
    //    {
    //        clientBuilder.AddStandardResilienceHandler();
    //    });
    //    // To output logs to the xUnit.net ITestOutputHelper, consider adding a package from https://www.nuget.org/packages?q=xunit+logging

    //    await using var app = await appHost.BuildAsync(CancellationToken.None);
    //    var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
    //    await app.StartAsync(CancellationToken.None);

    //    // Act
    //    var httpClient = app.CreateHttpClient("webfrontend");
    //    await resourceNotificationService.WaitForResourceAsync("webfrontend", KnownResourceStates.Running, CancellationToken.None).WaitAsync(TimeSpan.FromSeconds(30), CancellationToken.None);
    //    var response = await httpClient.GetAsync("/", CancellationToken.None);

    //    // Assert
    //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //}
}
