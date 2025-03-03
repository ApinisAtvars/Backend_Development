namespace Exercise3.Services;

public interface ICarService
{
    Task<Car> AddCar(Car car);
    Task<Car> GetCar(string id);
    Task<List<Car>> GetAllCars();
    Task SetupDummyData();
}

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IValidator<Car> _carValidator;
    public CarService(ICarRepository carRepository, IBrandRepository brandRepository, IValidator<Car> carValidator)
    {
        _carRepository = carRepository;
        _brandRepository = brandRepository;
        _carValidator = carValidator;
    }
    public Task<Car> AddCar(Car car)
    {
        var validationResult = _carValidator.Validate(car);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        return _carRepository.AddCar(car);
    }

    public Task<List<Car>> GetAllCars()
    {
        return _carRepository.GetAllCars();
    }

    public Task<Car> GetCar(string id)
    {
        return _carRepository.GetCar(id);
    }

    public async Task SetupDummyData()
    {
            if (!(await _brandRepository.GetAllBrands()).Any())
            {

                var brands = new List<Brand>(){
                new Brand()
                {
                Country = "Germany" , Name = "Volkswagen"
                },
                new Brand()
                {
            Country = "Germany" , Name = "BMW"
                },
                new Brand()
                {
            Country = "Germany" , Name = "Audi"
                },
                new Brand()
                {
                Country = "USA" , Name = "Tesla"
                }
            };

                foreach (var brand in brands)
                    await _brandRepository.AddBrand(brand);
            }

            if (!(await _carRepository.GetAllCars()).Any())
            {
                var brands = await _brandRepository.GetAllBrands();
                var cars = new List<Car>()
            {
                new Car(){

                    Name = "ID.3",
                    Brand = brands[0],
                },
                new Car(){

                    Name = "ID.4",
                    Brand = brands[0],
                },
                new Car(){

                    Name = "IX3",
                    Brand = brands[1],
                },
                new Car(){

                    Name = "E-Tron",
                    Brand = brands[2],
                },
                new Car(){

                    Name = "Model Y",
                    Brand = brands[3],
                }
            };
                foreach (var car in cars)
                    await _carRepository.AddCar(car);
        }
    }


    }