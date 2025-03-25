namespace Ex01_try2.Repositories;

public interface IMySqlRepository
{
    Task<List<Car>> GetCars();
    Task<Car> AddCar(Car car);
    Task<Registration> AddRegistration(Registration registration);
    Task<List<Registration>> GetRegistrations();
}

public class MySqlRepository : IMySqlRepository
{
    private readonly ApplicationContext _context;

    public MySqlRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetCars()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<Car> AddCar(Car car)
    {
        car = _context.Cars.Add(car).Entity;
        await _context.SaveChangesAsync();
        // Console.WriteLine(car.Id);
        return car;
        
    }

    public async Task<Registration> AddRegistration(Registration registration)
    {
        registration = _context.Registrations.Add(registration).Entity;
        await _context.SaveChangesAsync();
        return registration;
    }

    public async Task<List<Registration>> GetRegistrations()
    {
        return await _context.Registrations.ToListAsync();
    }

}