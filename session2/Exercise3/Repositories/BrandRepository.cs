namespace Exercise3.Repositories;

public interface IBrandRepository
{
    Task<List<Brand>> GetAllBrands();
    Task<Brand> AddBrand(Brand brand);
    Task<Brand> GetBrand(string id);
    Task<Brand> UpdateBrand(Brand brand);
}

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<Brand> _brands;
    public BrandRepository(IMongoContext context)
    {
        _brands = context.BrandsCollection;
    }
    public Task<Brand> AddBrand(Brand brand)
    {
        _brands.InsertOneAsync(brand);
        return Task.FromResult(brand);
    }

    public Task<List<Brand>> GetAllBrands()
    {
        return _brands.Find(_ => true).ToListAsync();
    }

    public Task<Brand> GetBrand(string id)
    {
        return _brands.Find(brand => brand.Id == id).FirstOrDefaultAsync();
    }

    public Task<Brand> UpdateBrand(Brand brand)
    {
        _brands.ReplaceOneAsync(b => b.Id == brand.Id, brand);
        return Task.FromResult(brand);
    }
}