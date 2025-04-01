namespace Sneakers.API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CustomerNr { get; set; }
        public float Discount { get; set; } = 0.0f;
        public string ApiKey { get; set; }
    }
}