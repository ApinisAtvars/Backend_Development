namespace EnergySystem.Api.Models;

public class Sensor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set;}
    public string Name {get; set;}
    public string Type {get; set;} // (Temperature, CO2, etc.)
    public string Unit {get; set;} // (Example: °C, °F, kWh, etc.)
}
