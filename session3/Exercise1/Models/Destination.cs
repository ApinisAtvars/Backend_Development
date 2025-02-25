namespace Exercise1.Models;

public class Destination
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Traveller> Travellers { get; set; }
}