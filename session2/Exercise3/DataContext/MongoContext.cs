using Microsoft.Extensions.Options;

namespace Exercise3.Contexts;

public interface IMongoContext
{
    IMongoCollection<Car> CarsCollection { get; }
    IMongoCollection<Brand> BrandsCollection { get; }
}


public class MongoContext : IMongoContext{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly ApplicationSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<ApplicationSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Car> CarsCollection
    {
        get
        {
            return _database.GetCollection<Car>(_settings.CarsCollection);
        }
    }

        public IMongoCollection<Brand> BrandsCollection
    {
        get
        {
            return _database.GetCollection<Brand>(_settings.BrandsCollection);
        }
    }
}

