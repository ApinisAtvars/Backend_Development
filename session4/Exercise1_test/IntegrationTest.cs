using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using Exercise1.DTOs;
using Exercise1.Models;
using Exercise1.Tests.Factories;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Exercise1.Tests.IntegrationTests;

public class IntegrationTest
{
    // [Fact]
    // public async Task CheckCarPrice()
    // {
    //     var factory = new MySqlFactory();
    //     var client = factory.CreateClient();
    //     // 1st, create a car
    //     var payload0 = new {
    //         id = 4,
    //         brand = "Toyota",
    //         model = "Corolla",
    //         plate = "ABC-123",
    //         color = "Red"
    //     };
    //     var response = await client.PostAsync("/cars", JsonContent.Create(payload0));
    //     response.EnsureSuccessStatusCode(); // Ensure the request was successful


    //     // Then, create a registration
    //     var payload = new 
    //     { 
    //         registrationId = 5, //This doesn't matter
    //         plate = "XYZ-987", 
    //         registrationStart = "2025-03-11T15:02:06", 
    //         registrationEnd = "2025-04-11T15:02:06", 
    //         carId = 4, 
    //         totalPrice = 100.0, // Fix large decimal issue
    //         isFinished = false
    //     };
    //     response = await client.PostAsync("/registrations", JsonContent.Create(payload));
    //     response.EnsureSuccessStatusCode(); // Ensure the request was successful


    //     // Then, PUT the end of the registration
    //     var payload2 = new { Id = 1, End = DateTime.Now.AddHours(1) };
    //     response = await client.PutAsync("/registrations", JsonContent.Create(payload2));
    //     response.EnsureSuccessStatusCode(); // Ensure the request was successful


    //     // Then, extract the cost from the response
    //     var jsonString = await response.Content.ReadAsStringAsync();
    //     var registrationResponse = JsonSerializer.Deserialize<RegistrationResponse>(jsonString, new JsonSerializerOptions
    //     {
    //         PropertyNameCaseInsensitive = true
    //     });

    //     // Validate that the price calculation is correct
    //     Assert.NotNull(registrationResponse);
    //     Assert.Equal(2, registrationResponse.TotalPrice);
    // }
    
    // public class RegistrationResponse
    // {
    //     public int RegistrationId { get; set; }
    //     public string Plate { get; set; }
    //     public DateTime RegistrationStart { get; set; }
    //     public DateTime RegistrationEnd { get; set; }
    //     public int CarId { get; set; }
    //     public double TotalPrice { get; set; }
    //     public bool IsFinished { get; set; }
    // }

    [Fact]
    public async Task GetAllCars()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        var response = await client.GetAsync("/cars");
        response.EnsureSuccessStatusCode(); // Ensure the request was successful

        var jsonString = await response.Content.ReadAsStringAsync();
        var cars = JsonSerializer.Deserialize<List<Car>>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(cars);
        Assert.NotEmpty(cars);
    }

    [Fact]
    public async Task PostCarTest()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        var payload = new {
            id = 4,
            brand = "Toyota",
            model = "Corolla",
            plate = "ABC-123",
            color = "Red",
            registrationId = 1
        };
        var response = await client.PostAsync("/cars", JsonContent.Create(payload));

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetCarsTest()
    {
        
        var factory = new MySqlFactory();
        var client = factory.CreateClient();

        var payload = new {
            id = 4,
            brand = "Toyota",
            model = "Corolla",
            plate = "ABC-123",
            color = "Red",
            registrationId = 5
        };
        var postResponse = await client.PostAsync("/cars", JsonContent.Create(payload));
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await client.GetAsync("/cars");
        getResponse.EnsureSuccessStatusCode();
        Assert.True(getResponse.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetRegistrations()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();
        var response = await client.GetAsync("/registrations");

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task PutRegistration()
    {
        var factory = new MySqlFactory();
        var client = factory.CreateClient();

        var payload0 = new {
            id = 4,
            brand = "Toyota",
            model = "Corolla",
            plate = "ABC-123",
            color = "Red"
        };
        var response = await client.PostAsync("/cars", JsonContent.Create(payload0));
        response.EnsureSuccessStatusCode(); // Ensure the request was successful

        
        // var payload = new {
        //     registrationId = 3, //This doesn't matter
        //     plate = "XYZ-987", 
        //     registrationStart = new DateTime(2025, 3, 11, 15, 2, 6), 
        //     registrationEnd = "2025-04-11T15:02:06", 
        //     carId = 4, 
        //     totalPrice = 100.0, // Fix large decimal issue
        //     isFinished = false
        // };

        RegistrationDTO payload = new RegistrationDTO
        {
            RegistrationId = 3,
            Plate = "XYZ-987",
            RegistrationStart = new DateTime(2025, 3, 11, 15, 2, 6),
            RegistrationEnd = new DateTime(2025, 3, 11, 15, 2, 6),
            CarId = 4,
            TotalPrice = 100,
            IsFinished = false
        };

        var postResponse = await client.PostAsync("/registrations", JsonContent.Create(payload));
        postResponse.EnsureSuccessStatusCode();


        var putResponse = await client.PutAsync("/registrations", JsonContent.Create(new {id=3, end="2025-03-11T16:02:06"}));
        putResponse.EnsureSuccessStatusCode();
        RegistrationDTO registration = await putResponse.Content.ReadFromJsonAsync<RegistrationDTO>();
        
        RegistrationDTO correctRegistration = new RegistrationDTO
        {
            RegistrationId = 3,
            Plate = "XYZ-987",
            RegistrationStart = new DateTime(2025, 3, 11, 15, 2, 6),
            RegistrationEnd = new DateTime(2025, 3, 11, 16, 2, 6),
            CarId = 4,
            TotalPrice = 2,
            IsFinished = true
        };

        Assert.Equal(correctRegistration.RegistrationId, registration.RegistrationId);
        Assert.Equal(correctRegistration.Plate, registration.Plate);
        Assert.Equal(correctRegistration.RegistrationStart, registration.RegistrationStart);
        Assert.Equal(correctRegistration.RegistrationEnd, registration.RegistrationEnd);
        Assert.Equal(correctRegistration.CarId, registration.CarId);
        Assert.Equal(correctRegistration.TotalPrice, registration.TotalPrice);
        Assert.Equal(correctRegistration.IsFinished, registration.IsFinished);

    }

}