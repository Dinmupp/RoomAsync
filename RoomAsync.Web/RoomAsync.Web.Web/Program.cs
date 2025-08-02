
using CompositionRoot;
using Microsoft.AspNetCore.Localization;
using RoomAsync.Web.Web;
using RoomAsync.Web.Web.Authentication;
using RoomAsync.Web.Web.Components;
using RoomAsync.Web.Web.Login;
using Serilog;
using System.Globalization;
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
builder.Services.AddDatabase(connectionString!, loggingDb!);


builder.Services.AddScoped<UserContextService>();


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

builder.Services.AddHttpClient<LoginApiClient>(client =>
{
    client.BaseAddress = new("https+http://apiservice");
});

builder.Services.AddHttpClient<AuthApiClient>(client =>
{
    client.BaseAddress = new("https+http://apiservice");
});


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("sv") };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.MapDefaultEndpoints();

app.Run();
