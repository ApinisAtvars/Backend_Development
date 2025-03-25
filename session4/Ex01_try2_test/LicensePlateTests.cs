using Ex01_try2.Models;
using Ex01_try2.Validators;

namespace Ex01_try2_test;

public class UnitTest1
{
    [Fact]
    public void IncorrectLicensePlate()
    {
        // Arrange
        Car car = new Car();
        car.Plate = "ABC123";
        CarValidator validator = new CarValidator();
        // Act
        var validationResult = validator.Validate(car);
        // Assert
        Assert.True(!validationResult.IsValid);
    }

    [Fact]
    public void CorrectLicensePlate()
    {
        // Arrange
        Car car = new Car();
        car.Plate = "ABC-123";
        CarValidator validator = new CarValidator();
        // Act
        var validationResult = validator.Validate(car);
        // Assert
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void CorrectLicensePlateVer2()
    {
        // Arrange
        Car car = new Car();
        car.Plate = "1-ABC-123";
        CarValidator validator = new CarValidator();
        // Act
        var validationResult = validator.Validate(car);
        // Assert
        Assert.True(validationResult.IsValid);
    }
}
