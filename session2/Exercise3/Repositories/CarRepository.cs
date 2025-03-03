namespace Exercise3.Repositories;

public interface ICarRepository
{
    Task<Car> AddCar(Car car);
    Task<Car> GetCar(string id);
    Task<List<Car>> GetAllCars();
}

public class CarRepository : ICarRepository
{
    private readonly IMongoCollection<Car> _cars;
    public CarRepository(IMongoContext context)
    {
        _cars = context.CarsCollection;
    }
    public Task<Car> AddCar(Car car)
    {
        _cars.InsertOneAsync(car);
        return Task.FromResult(car);
    }

    public Task<List<Car>> GetAllCars()
    {
        return _cars.Find(_ => true).ToListAsync();
    }

    public Task<Car> GetCar(string id)
    {
        return _cars.Find(car => car.Id == id).FirstOrDefaultAsync();
    }
}