namespace Exercise1.Models
{
    public class Passport
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; }
        // public int TravellerId { get; set; }
        // // Not required for this, but makes the traveller that has this passport apprear when it's queried
        public Traveller Traveller { get; set; }
    }
}