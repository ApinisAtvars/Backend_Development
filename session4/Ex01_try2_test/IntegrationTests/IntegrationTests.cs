namespace Ex01_try2_test.IntegrationTests;

public class IntegrationTests
{
    [Fact]
    public async Task GetAllCarsTest()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        var response = await client.GetAsync("/cars");
        response.EnsureSuccessStatusCode();

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetAllCarsCorrectLengthTest()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        var response = await client.GetAsync("/cars");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var cars = JsonConvert.DeserializeObject<List<CarDTO>>(json);

        Assert.NotNull(cars);
        Assert.NotEmpty(cars);
        Assert.Equal(3, cars.Count);

    }

    [Fact]
    public async Task PostCorrectCarTest()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        CarDTO car = new CarDTO
        {
            Brand = "Toyota",
            Model = "Corolla",
            Plate = "ABC-123",
            Color = "Red"
        };
        var json = JsonConvert.SerializeObject(car);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/cars", content);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PostCarIncorrectLicensePlate()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        CarDTO car = new CarDTO
        {
            Brand = "Toyota",
            Model = "Corolla",
            Plate = "ABC123",
            Color = "Red"
        };
        var json = JsonConvert.SerializeObject(car);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/cars", content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostCarInternalServerError()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        CarDTO car = new CarDTO
        {
            // Empty car, because validator only validates the license plate (I am lazy)
        };
        var json = JsonConvert.SerializeObject(car);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/cars", content);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
    [Fact]
    public async Task GetAllRegistrationsTest()
    {
        var factory     = new MySqlFactory();
        var client      = factory.CreateClient();
        var response    = await client.GetAsync("/registrations");
        
        response.EnsureSuccessStatusCode();

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetAllRegistrationsCorrectLengthTest()
    {
        var factory     = new MySqlFactory();
        var client      = factory.CreateClient();
        var response    = await client.GetAsync("/registrations");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var registrations = JsonConvert.DeserializeObject<List<RegistrationDTO>>(json);

        Assert.NotNull(registrations);
        Assert.NotEmpty(registrations);
        Assert.Equal(2, registrations.Count);

    }
    [Fact]
    public async Task PostCorrectRegistrationTest()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        RegistrationDTO registration = new RegistrationDTO
        {
            CarId = 3,
            Start = new DateTime(2022, 1, 1),
            End = new DateTime(2022, 1, 2)
        };
        var json = JsonConvert.SerializeObject(registration);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/registrations", content);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    [Fact]
    public async Task PutRegistrationAndCalculatePrice()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        RegistrationDTO registration = new RegistrationDTO
        {
            CarId = 3,
            Start = new DateTime(2025, 3, 25),
            End = new DateTime(2022, 1, 2) // Purposefully left the end date before the start date, because it's changed in the Service layer to the current datetime
        };
        var json = JsonConvert.SerializeObject(registration);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/registrations", content);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        response = await client.PutAsync("/registrations/3", null); // My PUT method doesn't need any json content, but whatever
        
        response.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // Extract the registration from the response
        var responseJson = await response.Content.ReadAsStringAsync();
        var updatedRegistration = JsonConvert.DeserializeObject<RegistrationDTO>(responseJson);

        decimal targetPrice = (decimal)(updatedRegistration.End - updatedRegistration.Start).TotalHours * 2;
        Assert.Equal(targetPrice, updatedRegistration.TotalPrice);

    }
}