
namespace Sneakers.API.Test;

public class BrandIntegrationTest: IAsyncLifetime
{
    private HttpClient _httpClient;
    private SneakerApiFactory _factory;
    private const string APIKEY = "usersecret";

    public BrandIntegrationTest()
    {
        /* This works, but there is a better way of doing it
        var factory = new SneakerApiFactory();
        _httpClient = factory.CreateClient();
        */
    }
    /*
    By doing this, Xunit will automatically initialie the factory and the http client, and get rid of them when the test is done
    */
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
    public async Task Get_Brands_Returns_Ok()
    {
        // Arrange
        var response = await _httpClient.GetAsync("/brands");

        // Act
        response.EnsureSuccessStatusCode();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Brands_Returns_Ok_With_Data()
    {
        var response = await _httpClient.GetAsync("/brands");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        var brands = JsonConvert.DeserializeObject<List<Brand>>(json);
        Assert.NotNull(brands);
        Assert.Equal(5, brands.Count);
    }

}