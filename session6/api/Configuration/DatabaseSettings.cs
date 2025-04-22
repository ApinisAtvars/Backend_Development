namespace EnergySystem.Api.Configuration;

public class DatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string BuildingCollection { get; set; }
    // public string SensorCollection { get; set; }
    public string SensorLogCollection { get; set; }
}