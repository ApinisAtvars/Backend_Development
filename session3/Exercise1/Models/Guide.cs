namespace Exercise1.Models
{
    public class Guide
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tour> Tours { get; set; }
    }
}