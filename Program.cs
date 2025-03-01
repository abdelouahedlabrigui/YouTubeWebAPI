using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.ApiKeys;
using YouTubeWebAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YouTubeWebAPI.Models.Trading.Alpaca;
using Alpaca.Markets;
using Microsoft.Extensions.Options;
using YouTubeWebAPI.Middlewares.Interfaces;
using YouTubeWebAPI.Middlewares.Providers;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddSingleton<IRouteProvider, RouteProvider>();

builder.Services.Configure<AlpacaSettings>(builder.Configuration.GetSection("AlpacaApi"));

builder.Services.Configure<HuggingFaceSettings>(
    builder.Configuration.GetSection("HuggingFace")
);

builder.Services.Configure<NasaApiSettings>(builder.Configuration.GetSection("NASA"));

builder.Services.AddSingleton<TextGenerationService>();

builder.Services.AddSingleton<IAlpacaTradingClient>(sp => {
    var options = sp.GetRequiredService<IOptions<AlpacaSettings>>().Value;
    return Alpaca.Markets.Environments.Paper
        .GetAlpacaTradingClient(new SecretKey(options.Key ?? string.Empty, options.Secret ?? string.Empty));
});

builder.Services.AddSingleton<IAlpacaDataClient>(sp => {
    var options = sp.GetRequiredService<IOptions<AlpacaSettings>>().Value;
    return Alpaca.Markets.Environments.Paper
        .GetAlpacaDataClient(new SecretKey(options.Key ?? string.Empty, options.Secret ?? string.Empty));
});

builder.Services.AddSingleton<AlpacaService>();

builder.Services.AddDbContext<AppDbContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
builder.Services.Configure<YouTubeApiSettings>(builder.Configuration.GetSection("YouTubeApiSettings"));

builder.Services.AddHttpClient<GoogleSearchService>();

builder.Services.AddHttpClient<NasaApiService>(client => {
    var config = builder.Configuration.GetSection("NASA").Get<NasaApiSettings>();
    client.BaseAddress = new Uri("https://api.nasa.gov/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
// builder.Services.AddHttpClient<NewsAPIService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSMLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5050")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpeechRecognitionLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5213")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowPyGenAILocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5005", "http://127.0.0.1:5005")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true) // Optional: Allow all origins dynamically.
            .AllowCredentials(); // If using credentials like cookies.

    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDjangoServices", policy =>
    {
        policy.WithOrigins("http://localhost:8000", "http://127.0.0.1:8000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true) // Optional: Allow all origins dynamically.
            .AllowCredentials(); // If using credentials like cookies.

    });
});

// 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeatherAPILocalhost", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5007")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGenAILocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5148")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSMLocalhost");
app.UseCors("AllowGenAILocalhost");
app.UseCors("AllowPyGenAILocalhost");
app.UseCors("AllowWeatherAPILocalhost");
app.UseCors("AllowDjangoServices");
app.UseCors("AllowSpeechRecognitionLocalhost");



// app.UseAuthorization();
app.MapControllers();   

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
