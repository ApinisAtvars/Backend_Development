using Exercise1.Services;
namespace Exercise1_test;

public class PriceCalculatorTest
{
    [Fact]
    public void TestCorrectPriceCalculation()
    {
        PriceCalculatorService priceCalculatorService = new PriceCalculatorService();
        DateTime from = new DateTime(2024,1,1,8,0,0);
        DateTime to = new DateTime(2024,1,1,9,0,0);

        Assert.Equal(2, priceCalculatorService.CalculatePrice(from, to));
    }

    [Fact]
    public void TestIncorrectPriceCalculation()
    {
        PriceCalculatorService priceCalculatorService = new PriceCalculatorService();
        DateTime from = new DateTime(2024,1,1,8,0,0);
        DateTime to = new DateTime(2024,1,1,9,0,0);

        Assert.NotEqual(3, priceCalculatorService.CalculatePrice(from, to));
    }
}