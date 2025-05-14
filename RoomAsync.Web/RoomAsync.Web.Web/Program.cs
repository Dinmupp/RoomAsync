
using CompositionRoot;
using RoomAsync.Web.Web;
using RoomAsync.Web.Web.Components;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read from appsettings.json
    .CreateLogger();

// Fix: UseSerilog is an extension method for IHostBuilder, not ConfigureHostBuilder.
// Access the IHostBuilder via builder.Host and cast it to IHostBuilder.
builder.Host.UseSerilog();

// Add services
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables();

var Configuration = configurationBuilder.Build();

var connectionString = Configuration.GetConnectionString("DefaultConnection");
var loggingDb = Configuration.GetConnectionString("LoggingDatabase");

//builder.Services.AddPrometheusExporter(".NET9");
builder.Services.AddLogger(Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddOAuth(Configuration.GetSection("OAuthConfig"));
builder.Services.AddApplication();
builder.Services.AddDatabase(connectionString!, loggingDb!);

//builder.Services.AddPrometheusExporter(".NET9");
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


//app.UseAuthentication();
//app.UseAuthorization();
//app.UseMiddleware<SessionMiddleware>();
//app.UseMiddleware<CorrelationIdMiddleware.CorrelationIdMiddleware>();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
