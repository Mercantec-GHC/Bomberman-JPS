using System.Diagnostics;
using System.Text;
using bomberman_backend.Data;
using bomberman_backend.Repository;
using bomberman_backend.Repository.Interfaces;
using bomberman_backend.Services;
using bomberman_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// GetAllHeadsets from the Postgres DB
IConfiguration Configuration = builder.Configuration;
var connectionString = Configuration.GetConnectionString("dbcontext") ??
                       Environment.GetEnvironmentVariable("dbcontext");
Console.WriteLine(connectionString);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContextcs>(options =>
{
    options.UseNpgsql(connectionString);
});


// Repos
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ILeaderboardRepo, LeaderboardRepo>();
builder.Services.AddScoped<IPowerUpRepo, PowerUpRepo>();
builder.Services.AddScoped<IPlayerRepo, PlayerRepo>();

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<IPowerUpService, PowerUpService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();


builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());


app.Run();
