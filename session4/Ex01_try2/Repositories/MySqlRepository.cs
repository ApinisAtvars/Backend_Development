namespace Ex01_try2.Repositories;

public interface IMySqlRepository
{
    Task<List<Car>> GetCars();
    Task<Car> AddCar(Car car);
    Task<Registration> AddRegistration(Registration registration);
    Task<List<Registration>> GetRegistrations();
    Task<Car> GetCar(int id);
    Task<Registration> PutRegistration(Registration registration);
    Task<Registration> GetRegistration(int id);
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
        List<Registration> registrations = await _context.Registrations.ToListAsync();
        foreach (Registration registration in registrations)
        {
            registration.Car = await GetCar(registration.CarId);
        }
        // Console.WriteLine(registrations);
        return registrations;
    }

    public async Task<Car> GetCar(int id)
    {
        return await _context.Cars.FindAsync(id);
    }

    public async Task<Registration> PutRegistration(Registration registration)
    {
        _context.Entry(registration).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return registration;
    }

    public async Task<Registration> GetRegistration(int id)
    {
        return await _context.Registrations.FindAsync(id);
    }

}