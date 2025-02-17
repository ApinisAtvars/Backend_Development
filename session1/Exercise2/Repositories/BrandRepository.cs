namespace Exercise2.Repositories;

public interface IBrandRepository
{
    List<Brand> GetBrands();
    List<Brand> GetBrandsFromCountry(string country);
    void AddBrand(Brand brand);
    Brand GetBrandById(int id);   
}

public class BrandRepository : IBrandRepository
{
    private static List<Brand> _brands = new List<Brand>();

    public void AddBrand(Brand brand)
    {
        brand.BrandId = _brands.Count + 1;
        _brands.Add(brand);
    }

    public Brand GetBrandById(int id)
    {
        return _brands.FirstOrDefault(x => x.BrandId == id);
    }
    public List<Brand> GetBrands()
    {
        return _brands;
    }

    public List<Brand> GetBrandsFromCountry(string country)
    {
        return _brands.Where(x => x.Country == country).ToList();
    }
}