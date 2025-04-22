namespace EnergySystem.Api.Exceptions;

public class BuildingNotFoundException : Exception
{
    public BuildingNotFoundException(string message) : base(message)
    {
    }
}