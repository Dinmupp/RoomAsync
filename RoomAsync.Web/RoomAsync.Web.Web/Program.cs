
using CompositionRoot;
using RoomAsync.Web.Web;
using RoomAsync.Web.Web.Components;
var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
//SerilogConfiguration.Configure(builder);

// Add services


var connectionString = "Server=localhost;Database=RoomAsync;TrustServerCertificate=True;Integrated Security=True;";
var loggingDb = "Server=localhost;Database=RoomAsync_LogLocalhost;TrustServerCertificate=True;Integrated Security=True;";


//builder.Services.AddPrometheusExporter(".NET9");
builder.Services.AddInfrastructure(connectionString, loggingDb, ".NET9");
builder.Services.AddOAuth(".NET9");
// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SessionMiddleware>();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
