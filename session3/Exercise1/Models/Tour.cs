namespace Exercise1.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; }
    }
}