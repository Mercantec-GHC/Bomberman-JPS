using Bomberman_Backend.Data;
using Bomberman_Backend.Repository;
using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Services;
using Bomberman_Backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Npgsql;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Testcontainers;
namespace Bomberman_Backend.Tests;
public class UserRepositoryIntegrationTest 
{
    private readonly PostgreSqlContainer postgressContainer = new PostgreSqlBuilder().Build();
    private string _connectionString = string.Empty;
    private WebApplicationFactory<Program> _factory;
    public DatabaseContext db { get; private set; } = default!;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        await postgressContainer.StartAsync();
        _connectionString = postgressContainer.GetConnectionString();
        _factory = new WebApplicationFactory<Program>()
    .WithWebHostBuilder(builder =>
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(_connectionString);
            });
        });
    });

        db = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
        db.Database.EnsureCreated();
    }

    [SetUp]
    public void Setup()
    {
        Console.WriteLine(_connectionString);
    }

    [Test]
    public async Task Add_Players_to_testcontainer_Database_And_Check_If_it_Persist()
    {
        Console.WriteLine(_connectionString);
        await using NpgsqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var players = new List<Player>();
        for (int i = 0; i < 10; i++)
        {
            players.Add(new Player
            {
                sessionId = new Session { },
                UserId = Guid.NewGuid(),
                UserName = $"user{i}",
                Email = $"test{i}@test.dk",
                Password = $"testpassword{i}",
            });
        }
            


        foreach (var player in players)
        {
            db.players.Add(player);
        }

        var playersFromDB = await db.players.ToListAsync();

        for (int i = 0; i < playersFromDB.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(playersFromDB[i].UserName, Is.EqualTo(players[i].UserName), $"Player {i} UserName does not match");
                Assert.That(playersFromDB[i].Email, Is.EqualTo(players[i].Email), $"Player {i} Email does not match");
            });
        }
    }

    [Test]
    public async Task Add_Players_to_testcontainer_Database_And_Check_If_it_Persist_Through_Repository()
    {
        Console.WriteLine(_connectionString);
        await using NpgsqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var players = new List<CreatePlayerDTO>();
        for (int i = 0; i < 10; i++)
        {
            players.Add(new CreatePlayerDTO
            {
                sessionId = new Session { },
                userName = $"user{i}",
                email = $"test{i}@test.dk",
                password = $"testpassword{i}",
            });
        }

        IPasswordHasher passwordHasher = new PasswordHash();
        var mockTokenProvider = new Mock<ITokenProvider>();
        var mockHttpContext = new Mock<IHttpContextAccessor>();
        IPlayerRepo playerRepo = new PlayerRepo(db, passwordHasher, mockTokenProvider.Object, mockHttpContext.Object);


        foreach (var player in players)
        {
            playerRepo.CreatePlayer(player);
        }

        var playersFromDB = playerRepo.GetPlayers();

        for (int i = 0; i < playersFromDB.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(playersFromDB[i].UserName, Is.EqualTo(players[i].userName), $"Player {i} UserName does not match");
                Assert.That(playersFromDB[i].Email, Is.EqualTo(players[i].email), $"Player {i} Email does not match");
            });
        }
    }

    [OneTimeTearDown]
     public async Task DisposeAsync()
    {
        await postgressContainer.DisposeAsync();
        await _factory.DisposeAsync();
        if(db != null)
        {
            await db.DisposeAsync();
        }
        
    }
}
