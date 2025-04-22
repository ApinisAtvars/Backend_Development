namespace EnergySystem.Api.Exceptions;

public class SensorLogNotFoundException : Exception
{
    public SensorLogNotFoundException(string message) : base(message)
    {
    }
}