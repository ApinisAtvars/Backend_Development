using Ex01_try2.Validators;

namespace Ex01_try2.Services;

public interface IMySqlService
{
    Task<List<Car>> GetCars();
    Task<Car> AddCar(Car car);
}

public class MySqlService : IMySqlService
{
    private readonly IMySqlRepository _repository;
    private readonly CarValidator _carValidator;

    public MySqlService(IMySqlRepository repository, CarValidator carValidator)
    {
        _repository = repository;
        _carValidator = carValidator;
    }

    public async Task<List<Car>> GetCars()
    {
        return await _repository.GetCars();
    }

    public async Task<Car> AddCar(Car car)
    {
        var validationResult = _carValidator.Validate(car);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(validationResult.Errors[0].ErrorMessage);
        }
        car = await _repository.AddCar(car);
        return car;
    }

    public async Task<Registration> AddRegistration(Registration registration)
    {
        return await _repository.AddRegistration(registration);
    }
    
}