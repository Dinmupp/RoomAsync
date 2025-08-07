using CompositionRoot;
using Domain;
using Microsoft.AspNetCore.Mvc;
using RoomAsync.Web.ApiService.Authentication;
using RoomAsync.Web.ApiService.Extensions;
using RoomAsync.Web.ApiService.Room;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDistributedMemoryCache(); // Eller Redis för lastbalansering
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
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

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RoomService>();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://roomasync.se")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();
app.UseRouting();
app.UseSession();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapPost("/login", async (
    HttpContext http,
    [FromServices] LoginService loginService,
    [FromBody] LoginRequest loginRequest,
    CancellationToken cancellationToken) =>
{
    var result = await loginService.LoginAsync(loginRequest.Username, loginRequest.Password, cancellationToken);

    if (result is null)
    {
        return Results.Unauthorized();
    }


    http.Session.SetObject("UserContext", result);


    return Results.Ok(result);
})
.WithName("LoginUser");

app.MapPost("/getallrooms", async (
    HttpContext http,
    [FromServices] RoomService roomService,
    [FromBody] GetAllRoomsRequest request,
    CancellationToken cancellationToken) =>
{
    var result = await roomService.GetAllAsync(request, cancellationToken);

    if (result is null)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(result);
})
.WithName("GetAllRooms");



app.MapGet("/me", (HttpContext http) =>
{
    var user = http.Session.GetObject<UserContext>("UserContext");

    return Results.Ok(user);
});


app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class GetAllRoomsRequest
{
    public string? RoomNumber { get; set; }
    public int? StartIndex { get; set; }
    public int? Count { get; set; }
    public string? SortBy { get; set; }
    public bool SortByAscending { get; set; }
}
