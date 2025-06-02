using Bomberman_Backend.Data;
using Bomberman_Backend.Repository;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
namespace Bomberman_Backend.Tests.UnitTest.Repositories;

public class UserRepositoryTest
{
    
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Add_Players_To_Database()
    {
        
        var mockPlayerDbSet = new Mock<DbSet<Player>>();
        var DbOptions = new DbContextOptions<DatabaseContext>();
        var mockDbContext = new Mock<DatabaseContext>(DbOptions);

        mockDbContext.Setup(db => db.players).Returns(mockPlayerDbSet.Object);

        IList<CreatePlayerDTO> players = new List<CreatePlayerDTO>();
        for (int i = 0; i < 10; i++)
        {
            players.Add(new CreatePlayerDTO
            {
                sessionId = new Session { Id = i},
                userName = $"user{i}",
                email = $"user{i}@test.dk",
                password = $"testpassword{i}",
            });
        }

        
        IPasswordHasher passwordHasher = new PasswordHash();
        var mockTokenProvider = new Mock<ITokenProvider>();
        var mockHttpContext = new Mock<IHttpContextAccessor>();

        IPlayerRepo playerRepo = new PlayerRepo(mockDbContext.Object, passwordHasher, mockTokenProvider.Object, mockHttpContext.Object);

        mockPlayerDbSet.Setup(m => m.Add(It.IsAny<Player>()))
            .Callback<Player>(p => p.Id = new Random().Next(1, 10000));

        foreach (var player in players)
        {
            playerRepo.CreatePlayer(player);
        }

        mockPlayerDbSet.Verify(m => m.Add(It.IsAny<Player>()), Times.Exactly(10));
        mockDbContext.Verify(m => m.SaveChanges(), Times.Exactly(10));

    }

    [Test]
    public void Get_All_Users()
    {
        var mockUserDbSet = new Mock<DbSet<User>>();
        var DbOptions = new DbContextOptions<DatabaseContext>();
        var mockDbContext = new Mock<DatabaseContext>(DbOptions);




        var users = new List<User>();
     for (int i = 0; i < 10; i++)
        {
            users.Add(new User
            {
                Id = i,
                UserId = Guid.NewGuid(),
                UserName = $"user{i}",
                Email = $"test{i}@test.dk",
                Password = $"testpassword{i}"
            });
        }
        mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
        mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
        mockUserDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
        mockUserDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
        mockDbContext.Setup(db => db.users).Returns(mockUserDbSet.Object);
        IUserRepo userRepo = new UserRepo(mockDbContext.Object);
        var userList = userRepo.GetUsers();
        Assert.AreEqual(10, userList.Count);
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual($"user{i}", userList[i].UserName);
            Assert.AreEqual($"test{i}@test.dk", userList[i].Email);
            Assert.AreEqual(i, userList[i].Id);
        }
    }
}
