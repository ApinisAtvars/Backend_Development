using Exercise1.Services;
using Exercise1.Repositories;
using Exercise1.Contexts;
using Exercise1.Models;
using Exercise1.Validators;
using Exercise1.Exceptions;

using Exercise1.Tests.Factories;
using System.Text.Json;

namespace Exercise1_test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

        Assert.True(true);
    }
    [Fact]
    public void CorrectLicensePlateVersionOne()
    {
        var carValidator = new CarValidator();
        Registration registration = new Registration();
        var car = new Car
        {
            Plate = "ABC-123", // One correct version of license plate
            Brand = "Toyota",
            Model = "Corolla",
            Registration = registration,
            Color = "Red"
        };
        
        var validationResult = carValidator.Validate(car);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void CorrectLicensePlateVersionTwo()
    {
        var carValidator = new CarValidator();
        Registration registration = new Registration();
        var car = new Car
        {
            Plate = "1-ABC-234", // One correct version of license plate
            Brand = "Toyota",
            Model = "Corolla",
            Registration = registration,
            Color = "Red"
        };
        
        var validationResult = carValidator.Validate(car);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void IncorrectLicensePlate()
    {
        var carValidator = new CarValidator();
        Registration registration = new Registration();
        var car = new Car
        {
            Plate = "1", // One correct version of license plate
            Brand = "Toyota",
            Model = "Corolla",
            Registration = registration,
            Color = "Red"
        };
        
        var validationResult = carValidator.Validate(car);
        Assert.True(!validationResult.IsValid);
    }
}
