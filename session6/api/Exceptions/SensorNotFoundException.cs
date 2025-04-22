namespace EnergySystem.Api.Exceptions;

public class SensorNotFoundException : Exception
{
    public SensorNotFoundException(string message) : base(message)
    {
    }
}