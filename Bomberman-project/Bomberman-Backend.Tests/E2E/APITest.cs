using DomainModels;
using DomainModels.DTO;
using System.Net.Http.Json;

namespace Bomberman_Backend.Tests;

public class APITest
{
    private HttpClient _client;
    private readonly string _baseUrl = "http://10.133.51.104:8080"; // Adjust the base URL as needed

    [SetUp]
    public void Setup()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl)
        };
    }

    [Test]
    public void Create_Players()
    {
        var newPlayer = new CreatePlayerDTO
        {
            userName = "testuser101",
            email = "test100@test.dk",
            password = "testpassword101",
            sessionId = new Session { } // Assuming Session has an Id property
        };

        var response = _client.PostAsJsonAsync("api/Player", newPlayer).Result;
        Assert.IsTrue(response.IsSuccessStatusCode, "Failed to create player");
    }

    [Test]
    public async Task Get_All_Players()
    {
        var response = await _client.GetFromJsonAsync<List<Player>>("api/Player/all");
        response.ForEach(player =>
        {
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(player.Id, "Player ID should not be null");
                Assert.IsNotNull(player.UserName, "Player UserName should not be null");
                Assert.IsNotNull(player.Email, "Player Email should not be null");
            });
        });
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }
}
