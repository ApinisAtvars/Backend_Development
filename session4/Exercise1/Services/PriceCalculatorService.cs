namespace Exercise1.Services;

public interface IPriceCalculatorService
{
    decimal CalculatePrice(DateTime entry, DateTime exit);
}
public class PriceCalculatorService : IPriceCalculatorService
{
    public decimal CalculatePrice(DateTime entry, DateTime exit)
    {
        decimal result = exit.Subtract(entry).Hours * 2;
        return result;
        
    }
}