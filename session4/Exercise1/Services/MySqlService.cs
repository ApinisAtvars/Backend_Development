namespace Exercise1.Services;

public interface IMySqlService
{
    Task<List<Car>> GetCars();
    Task<List<Registration>> GetRegistrations();
    Task AddRegistration(Registration registration);
    Task<Registration> EditRegistration(Registration registration);
    Task AddCar(Car car);
    // Task<float> CalculateTotalPrice(Registration registration);
}

public class MySqlService : IMySqlService
{
    private readonly IMySqlRepository _repository;
    private readonly RegistrationValidator _registrationValidator;
    private readonly CarValidator _carValidator;
    private readonly CarValidationException _carValidationException;
    private readonly RegistrationValidationException _registrationValidationException;

    public MySqlService(IMySqlRepository repository, RegistrationValidator registrationValidator, CarValidator carValidator)
    {
        _repository = repository;
        _registrationValidator = registrationValidator;
        _carValidator = carValidator;
    }

    public async Task<List<Car>> GetCars()
    {
        return await _repository.GetCars();
    }

    public async Task<List<Registration>> GetRegistrations()
    {
        return await _repository.GetRegistrations();
    }

    public async Task AddRegistration(Registration registration)
    {
        var validationResult = _registrationValidator.Validate(registration);
        if (!validationResult.IsValid)
        {
            throw new RegistrationValidationException(validationResult.Errors);
        }
        await _repository.AddRegistration(registration);
        return;
    }

    public async Task<Registration> EditRegistration(Registration registration)
    {
        return await _repository.EditRegistration(registration);
    }

    public async Task AddCar(Car car)
    {
        var validationResult = _carValidator.Validate(car);
        if (!validationResult.IsValid)
        {
            throw new CarValidationException(validationResult.Errors);
        }
        await _repository.AddCar(car);
        return;
    }



}