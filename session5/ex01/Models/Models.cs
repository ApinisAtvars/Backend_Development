namespace Sneakers.API.Models;

public class Brand
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BrandId { get; set; }
    public string Name { get; set; }
}


public class Occasion
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OccasionId { get; set; }
    public string Description { get; set; }
}

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OrderId { get; set; }
    public string Email { get; set; }
    public string SneakerId { get; set; }
    public int NumberOfItems { get; set; }
}

public class Sneaker
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SneakerId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Brand Brand { get; set; }
    public List<Occasion> Occasions { get; set; }
}

