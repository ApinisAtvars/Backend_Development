namespace EnergySystem.Api.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Building> BuildingCollection { get; }
    IMongoCollection<SensorLog> SensorLogCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }

    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Building> BuildingCollection
    {
        get
        {
            return _database.GetCollection<Building>(_settings.BuildingCollection);
        }
    }

    public IMongoCollection<SensorLog> SensorLogCollection
    {
        get
        {
            return _database.GetCollection<SensorLog>(_settings.SensorLogCollection);
        }
    }

}