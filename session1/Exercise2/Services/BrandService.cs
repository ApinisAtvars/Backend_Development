namespace Exercise2.Services;

public interface IBrandService
{
    List<Brand> GetBrands();
    List<Brand> GetBrandsFromCountry(string country);
    void AddBrand(Brand brand);
    Brand GetBrandById(int id);
}

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly BrandValidator _brandValidator;

    public BrandService(IBrandRepository brandRepository, BrandValidator brandValidator)
    {
        _brandRepository = brandRepository;
        _brandValidator = brandValidator;
    }

    public void AddBrand(Brand brand)
    {
        var validationResults = _brandValidator.Validate(brand);
        if (!validationResults.IsValid)
        {
            throw new CustomValidationException(validationResults.Errors);
        }
        _brandRepository.AddBrand(brand);
    }

    public Brand GetBrandById(int id)
    {
        return _brandRepository.GetBrandById(id);
    }

    public List<Brand> GetBrands()
    {
        return _brandRepository.GetBrands();
    }

    public List<Brand> GetBrandsFromCountry(string country)
    {
        return _brandRepository.GetBrandsFromCountry(country);
    }
}