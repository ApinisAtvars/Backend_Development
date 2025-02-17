namespace Exercise2.Services;

public interface ICarModelService
{
    List<CarModel> GetCarModels();
    List<CarModel> GetCarModelsFromBrand(int brandId);
    void AddCarModel(CarModel carModel);
    CarModel GetCarModelById(int id);
    List<CarModel> GetCarModelsFromCountry(string country);
}

public class CarModelService : ICarModelService
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly CarModelValidator _carModelValidator;

    public CarModelService(ICarModelRepository carModelRepository, IBrandRepository brandRepository, CarModelValidator carModelValidator)
    {
        _carModelRepository = carModelRepository;
        _brandRepository = brandRepository;
        _carModelValidator = carModelValidator;
    }

    public List<CarModel> GetCarModels()
    {
        return _carModelRepository.GetCarModels();
    }

    public List<CarModel> GetCarModelsFromBrand(int brandId)
    {
        Brand brand = _brandRepository.GetBrandById(brandId);
        return _carModelRepository.GetCarModelsFromBrand(brand);
    }

    public void AddCarModel(CarModel carModel)
    {
        var validationResults = _carModelValidator.Validate(carModel);
        if (!validationResults.IsValid)
        {
            throw new CustomValidationException(validationResults.Errors);
        }
        _carModelRepository.AddCarModel(carModel);
    }

    public CarModel GetCarModelById(int id)
    {
        return _carModelRepository.GetCarModelById(id);
    }

    public List<CarModel> GetCarModelsFromCountry(string country)
    {
        return _carModelRepository.GetCarModelsFromCountry(country);
    }
}