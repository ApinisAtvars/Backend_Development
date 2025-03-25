namespace Ex01_try2.Models;

public class Registration
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int CarId { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsFinished { get; set; }
    public Car Car { get; set; }
}