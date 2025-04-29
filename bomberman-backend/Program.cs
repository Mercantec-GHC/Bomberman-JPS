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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContextcs>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("dbcontext"));
});

// Repos
builder.Services.AddScoped<IUserRepo, UserRepo>();

//Services
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

app.Run();
