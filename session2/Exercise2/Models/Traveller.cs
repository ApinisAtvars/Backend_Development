namespace Exercise2.Models;

public class Traveller
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int PassportId { get; set; }   
    public Passport Passport { get; set; }
    public List<Destination> Destinations { get; set; }

}