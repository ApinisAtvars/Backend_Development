using System.Text;
using Sneakers.API.DTOs;

namespace Sneakers.API.Test;

public class SneakersIntegrationTest : IAsyncLifetime
{
    private HttpClient _httpClient;
    private SneakerApiFactory _factory;
    private const string APIKEY = "usersecret";

    public async Task InitializeAsync()
    {
        _factory = new SneakerApiFactory();
        await _factory.InitializeAsync();
        _httpClient = _factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Add("XApiKey", APIKEY);
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }

    [Fact]
    public async Task Get_Sneakers_Returns_Ok()
    {
        // Arrange
        var response = await _httpClient.GetAsync("/sneakers");

        // Act
        response.EnsureSuccessStatusCode();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_Sneakers_Returns_Created()
    {
        // 2. Get a brand from the dummy data
        var brandsResponse = await _httpClient.GetAsync("/brands");
        brandsResponse.EnsureSuccessStatusCode();
        var brandsJson = await brandsResponse.Content.ReadAsStringAsync();
        var brands = JsonConvert.DeserializeObject<List<Brand>>(brandsJson);
        var firstBrand = brands.First();

        // 3. Get an occasion from the dummy data
        var occasionsResponse = await _httpClient.GetAsync("/occasions");
        occasionsResponse.EnsureSuccessStatusCode();
        var occasionsJson = await occasionsResponse.Content.ReadAsStringAsync();
        var occasions = JsonConvert.DeserializeObject<List<Occasion>>(occasionsJson);
        var firstOccasion = occasions.First();

        // 4. Create a new sneaker
        var newSneaker = new NewSneakerDTO
        {
            Name = "Test Sneaker",
            Price = 100,
            Stock = 10,
            Brand = firstBrand,
            Occasions = new List<Occasion>{firstOccasion}
        };

        var json = JsonConvert.SerializeObject(newSneaker);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/sneakers", content);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        
        
    }
}