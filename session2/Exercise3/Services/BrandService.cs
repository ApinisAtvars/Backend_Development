namespace Exercise3.Services;

public interface IBrandService
{
    Task<Brand> AddBrand(Brand brand);
    Task<Brand> GetBrand(string id);
    Task<List<Brand>> GetAllBrands();
}

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IValidator<Brand> _brandValidator;
    public BrandService(IBrandRepository brandRepository, IValidator<Brand> brandValidator)
    {
        _brandRepository = brandRepository;
        _brandValidator = brandValidator;
    }
    public Task<Brand> AddBrand(Brand brand)
    {
            var validationResult = _brandValidator.Validate(brand);
            if (!validationResult.IsValid){
                throw new FluentValidation.ValidationException(validationResult.Errors);
                }
            return _brandRepository.AddBrand(brand);        
    }

    public Task<List<Brand>> GetAllBrands()
    {
        return _brandRepository.GetAllBrands();
    }

    public Task<Brand> GetBrand(string id)
    {
        return _brandRepository.GetBrand(id);
    }
}