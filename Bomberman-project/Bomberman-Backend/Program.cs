using System.Reflection;
using System.Text;
using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Bomberman_Backend.Services.Interfaces;
using Bomberman_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = builder.Configuration;
var connectionString = Configuration.GetConnectionString("dbcontext") ??
                      Environment.GetEnvironmentVariable("dbcontext");

var issuer = Configuration["Jwt:Issuer"] ??
             Environment.GetEnvironmentVariable("issuer");
var audience = Configuration["Jwt:Audience"] ??
               Environment.GetEnvironmentVariable("audience");
var secret = Configuration["Jwt:Secret"] ??
             Environment.GetEnvironmentVariable("secret");


builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes =
        ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 1024 * 1024 * 10; // 10 MB
});

// Add services to the container.
// GetAllHeadsets from the Postgres DB


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(connectionString);
});


// Repos
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ILeaderboardRepo, LeaderboardRepo>();
builder.Services.AddScoped<IPowerUpRepo, PowerUpRepo>();
builder.Services.AddScoped<IPlayerRepo, PlayerRepo>();
builder.Services.AddScoped<ILobbyRepo, LobbyRepo>();
builder.Services.AddScoped<IPasswordHasher, PasswordHash>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();


//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<IPowerUpService, PowerUpService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ILobbyService, LobbyService>();


builder.Services.AddHealthChecks();



builder.Services.AddSwaggerGen(
    options =>     {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bomberman API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    }
);

builder.Services.AddAuthorization();

var app = builder.Build();


app.MapHealthChecks("/healthz");

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.MapControllers();


app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.Run();
