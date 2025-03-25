using Ex01_try2.Validators;

namespace Ex01_try2.Services;

public interface IMySqlService
{
    Task<List<CarDTO>> GetCars();
    Task<CarDTO> AddCar(CarDTO car);
    Task<List<RegistrationDTO>> GetRegistrations();
    Task<RegistrationDTO> AddRegistration(RegistrationDTO registration);
    Task<RegistrationDTO> PutRegistration(int registrationId);
}

public class MySqlService : IMySqlService
{
    private readonly IMySqlRepository _repository;
    private readonly CarValidator _carValidator;
    private readonly IMapper _mapper;


    public MySqlService(IMySqlRepository repository, CarValidator carValidator, IMapper mapper)
    {
        _repository = repository;
        _carValidator = carValidator;
        _mapper = mapper;
    }

    public async Task<List<CarDTO>> GetCars()
    {
        List<Car> cars = await _repository.GetCars();
        var carsDTO = _mapper.Map<List<CarDTO>>(cars);
        return carsDTO;

    }

    public async Task<CarDTO> AddCar(CarDTO car)
    {
        Car remappedCar = _mapper.Map<Car>(car);
        var validationResult = _carValidator.Validate(remappedCar);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(validationResult.Errors[0].ErrorMessage);
        }
        remappedCar = await _repository.AddCar(remappedCar);
        car = _mapper.Map<CarDTO>(remappedCar);
        return car;
    }

    public async Task<RegistrationDTO> AddRegistration(RegistrationDTO registration)
    {
        Registration remappedRegistration = _mapper.Map<Registration>(registration);
        remappedRegistration = await _repository.AddRegistration(remappedRegistration); 
        return _mapper.Map<RegistrationDTO>(remappedRegistration);
    }

    public async Task<List<RegistrationDTO>> GetRegistrations()
    {
        List<Registration> registrations = await _repository.GetRegistrations();

        var registrationsDTO = _mapper.Map<List<RegistrationDTO>>(registrations);
        return registrationsDTO;
    }
    public async Task<decimal> CalculatePrice(DateTime start, DateTime end)
    {
        return (decimal)(end - start).TotalHours * 2;
    }

    public async Task<RegistrationDTO> PutRegistration(int registrationId)
    {
        Registration registration = await _repository.GetRegistration(registrationId);
        registration.IsFinished = true;
        registration.End = DateTime.Now;
        registration.TotalPrice = await CalculatePrice(registration.Start, registration.End);
        registration = await _repository.PutRegistration(registration);

        return _mapper.Map<RegistrationDTO>(registration);
    }
    
}