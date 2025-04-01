namespace EnergySystem.Api.Models;

public class SensorLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string BuildingId { get; set; }
    public string SensorId { get; set; }
    public float Value { get; set; }
    public string Unit { get; set; }
    public DateTime Timestamp { get; set; }
}