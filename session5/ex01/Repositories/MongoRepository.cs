namespace Sneakers.API.Repositories;

public interface IMongoRepository
{
    Task AddBrand(Brand brand);
    Task<List<Brand>> GetAllBrands();
    Task<List<Occasion>> GetAllOccasions();
    Task AddSneaker(Sneaker newSneaker);
    Task AddBrands(List<Brand> brands);
    Task<List<Sneaker>> GetAllSneakers();
    Task AddOccasions(List<Occasion> occasions);
}

public class MongoRepository : IMongoRepository
{
    private readonly IMongoCollection<Brand> _brands;
    private readonly IMongoCollection<Occasion> _occasions;
    private readonly IMongoCollection<Order> _orders;
    private readonly IMongoCollection<Sneaker> _sneakers;

    public MongoRepository(IMongoContext context)
    {
        _brands = context.BrandsCollection;
        _occasions = context.OccasionCollection;
        _orders = context.OrdersCollection;
        _sneakers = context.SneakerCollection;
    }

    public async Task AddBrand(Brand brand)
    {
        await _brands.InsertOneAsync(brand);
    }

    public async Task<List<Brand>> GetAllBrands()
    {
        return await _brands.Find(_ => true).ToListAsync();
    }

    public async Task<List<Occasion>> GetAllOccasions()
    {
        return await _occasions.Find(_ => true).ToListAsync();
    }
    
    public async Task AddSneaker(Sneaker newSneaker)
    {
        await _sneakers.InsertOneAsync(newSneaker);
    }

    public async Task AddBrands(List<Brand> brands)
    {
        await _brands.InsertManyAsync(brands);
    }

    public async Task<List<Sneaker>> GetAllSneakers()
    {
        return await _sneakers.Find(_ => true).ToListAsync();
    }

    public async Task AddOccasions(List<Occasion> occasions)
    {
        await _occasions.InsertManyAsync(occasions);
    }
    
}