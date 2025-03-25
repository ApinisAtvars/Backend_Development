namespace Exercise1.Repositories;

public interface IMySqlRepository
{
    Task<List<Car>> GetCars();
    Task<List<Registration>> GetRegistrations();
    Task AddRegistration(Registration registration);
    Task<Registration> EditRegistration(Registration registration);
    Task AddCar(Car car);
}

public class MySqlRepository : IMySqlRepository
{
    private readonly ApplicationContext _context;

    public MySqlRepository(ApplicationContext applicationContext)
    {
        _context = applicationContext;
    }

    public async Task<List<Car>> GetCars()
    {
        return await _context.Cars
        .Include(c => c.Registration)
        .ToListAsync();
    }

    public async Task AddCar(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<List<Registration>> GetRegistrations()
    {
        return await _context.Registrations
        .Include(r => r.Car)
        .ToListAsync();
    }

    public async Task AddRegistration(Registration registration)
    {
        await _context.Registrations.AddAsync(registration);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<Registration> GetRegistrationById(int id)
    {
        return await _context.Registrations
        .Include(r => r.Car)
        .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Registration> EditRegistration(Registration registration)
    {
        Registration registrationToEdit = await GetRegistrationById(registration.Id);
        registrationToEdit.IsFinished = true;
        registrationToEdit.TotalPrice = (decimal)(registration.End - registrationToEdit.Start).TotalHours * 2;
        registrationToEdit.End = registration.End;
        // registration.IsFinished = true;
        // registration.TotalPrice = registration.End.Subtract(registration.Start).Hours * 100;
        _context.Registrations.Update(registrationToEdit);
        await _context.SaveChangesAsync();
        return registrationToEdit;
    }
}