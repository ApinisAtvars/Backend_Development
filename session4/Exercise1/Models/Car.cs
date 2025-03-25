namespace Exercise1.Models;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
    public string Color { get; set; }
    public int RegistrationId { get; set; }
    public Registration Registration { get; set; }
}