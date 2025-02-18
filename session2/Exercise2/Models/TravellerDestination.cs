namespace Exercise2.Models;

public class TravellerDestination
{
    [ForeignKey("Traveller")]
    public int TravellerId { get; set; }
    public Traveller Traveller { get; set; }
    [ForeignKey("Destination")]
    public int DestinationId { get; set; }
    public Destination Destination { get; set; }
}