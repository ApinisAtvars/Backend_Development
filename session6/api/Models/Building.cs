namespace EnergySystem.Api.Models;

public class Building
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set;}
    public string Name {get; set;}
    public string Address {get; set;}
    public string City {get; set;}
    public string Country {get; set;}
    public string PostalCode {get; set;}
    public List<Sensor> Sensors {get; set;}
}