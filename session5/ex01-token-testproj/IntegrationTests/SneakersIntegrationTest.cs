using System.Text;
using Sneakers.API.DTOs;

namespace Sneakers.API.Test;

public class TokenResponse
{
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}

public class SneakersIntegrationTest : IAsyncLifetime
{
    private HttpClient _httpClient;
    private SneakerApiFactory _factory;
    private string _authToken;

    public async Task InitializeAsync()
    {
        _factory = new SneakerApiFactory();
        await _factory.InitializeAsync();
        _httpClient = _factory.CreateClient();
        // Get the authentication token, and store it in the _authToken variable
        // var tokenResponse = await GetAuthTokenAsync();
        _authToken = await GetAuthTokenAsync();
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }

    // Method to get the authentication token
    private async Task<string> GetAuthTokenAsync()
    {
        var loginRequest = new
        {
            email = "testuser@test.com",
            password = "Test123!"
        };

        var json = JsonConvert.SerializeObject(loginRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/login", content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var tokenResponse = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);
        Assert.NotNull(token);
        return token.accessToken;
    }


    [Fact]
    public async Task Get_Sneakers_Returns_Ok()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);
        var response = await _httpClient.GetAsync("/sneakers");

        // Act
        response.EnsureSuccessStatusCode();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // Not Implemented

    // [Fact]
    // public async Task Post_Sneakers_Returns_Created()
    // {
    //     // 2. Get a brand from the dummy data
    //     var brandsResponse = await _httpClient.GetAsync("/brands");
    //     brandsResponse.EnsureSuccessStatusCode();
    //     var brandsJson = await brandsResponse.Content.ReadAsStringAsync();
    //     var brands = JsonConvert.DeserializeObject<List<Brand>>(brandsJson);
    //     var firstBrand = brands.First();

    //     // 3. Get an occasion from the dummy data
    //     var occasionsResponse = await _httpClient.GetAsync("/occasions");
    //     occasionsResponse.EnsureSuccessStatusCode();
    //     var occasionsJson = await occasionsResponse.Content.ReadAsStringAsync();
    //     var occasions = JsonConvert.DeserializeObject<List<Occasion>>(occasionsJson);
    //     var firstOccasion = occasions.First();

    //     // 4. Create a new sneaker
    //     var newSneaker = new NewSneakerDTO
    //     {
    //         Name = "Test Sneaker",
    //         Price = 100,
    //         Stock = 10,
    //         Brand = firstBrand,
    //         Occasions = new List<Occasion>{firstOccasion}
    //     };

    //     var json = JsonConvert.SerializeObject(newSneaker);
    //     var content = new StringContent(json, Encoding.UTF8, "application/json");
    //     var response = await _httpClient.PostAsync("/sneakers", content);
    //     response.EnsureSuccessStatusCode();
    //     Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        
        
    // }
}