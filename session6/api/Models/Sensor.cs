namespace EnergySystem.Api.Models;

public class Sensor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set;}
    public string Name {get; set;}
    public string Type {get; set;}
    public string Unit {get; set;}
}
