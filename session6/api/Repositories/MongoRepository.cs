namespace EnergySystem.Api.Repositories;

public interface IMongoRepository
{
    // TODO: GetStatsForSensorLog
    // The result contains the min, max and average of the sensor logs for the given building and sensor.
    Task<string> AddBuilding(Building building);
    Task<List<Building>> GetBuildings();
    Task<Building> GetBuilding(string id);
    Task AddSensorToBuilding(string buildingId, Sensor sensor);
    Task<string> AddSensorLog(SensorLog sensorLog);
    Task<List<float>> GetStatsForSensorLog(string buildingId, string sensorId);
    Task<List<SensorLog>> GetSensorLogByBuildingAndSensor(string buildingId, string sensorId);
    Task<List<Sensor>> GetSensorsForBuilding(string buildingId); // To check whether the building even has sensors assigned to it (in the service layer)
    Task<List<SensorLog>> GetSensorLogs();
}

public class MongoRepository : IMongoRepository
{
    private readonly IMongoCollection<Building> _buildingsCollection;
    private readonly IMongoCollection<SensorLog> _sensorLogsCollection;
    public MongoRepository(IMongoContext context)
    {
        _buildingsCollection = context.BuildingCollection;
        _sensorLogsCollection = context.SensorLogCollection;
    }

    public async Task<string> AddBuilding(Building building)
    {
        await _buildingsCollection.InsertOneAsync(building);
        return building.Id;
    }

    public async Task<string> AddSensorLog(SensorLog sensorLog)
    {
        await _sensorLogsCollection.InsertOneAsync(sensorLog);
        return sensorLog.Id;
        
    }

    public async Task AddSensorToBuilding(string buildingId, Sensor sensor)
    {
        sensor.Id = ObjectId.GenerateNewId().ToString();
        var building = await GetBuilding(buildingId);
        building.Sensors.Add(sensor);
        await _buildingsCollection.ReplaceOneAsync(b => b.Id == buildingId, building);
    }

    public async Task<Building> GetBuilding(string id)
    {
        return await _buildingsCollection.Find(Building => Building.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Building>> GetBuildings()
    {
        return await _buildingsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<List<SensorLog>> GetSensorLogByBuildingAndSensor(string buildingId, string sensorId)
    {
        var filter = Builders<SensorLog>.Filter.And(
            Builders<SensorLog>.Filter.Eq(log => log.BuildingId, buildingId),
            Builders<SensorLog>.Filter.Eq(log => log.SensorId, sensorId)
        );
        return await _sensorLogsCollection.Find(filter).ToListAsync();
    }

    public async Task<List<Sensor>> GetSensorsForBuilding(string buildingId)
    {
        var building = await _buildingsCollection.Find(b => b.Id == buildingId).FirstOrDefaultAsync();
        return building.Sensors;
    }


    public async Task<List<SensorLog>> GetSensorLogs()
    {
        return await _sensorLogsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<List<float>> GetStatsForSensorLog(string buildingId, string sensorId)
    {
        var filter = Builders<SensorLog>.Filter.And(
            Builders<SensorLog>.Filter.Eq(log => log.BuildingId, buildingId),
            Builders<SensorLog>.Filter.Eq(log => log.SensorId, sensorId)
        );
        var sensorLogs = await _sensorLogsCollection.Find(filter).ToListAsync();
        
        var stats = new List<float>();
        stats.Add(sensorLogs.Min(log => log.Value)); // Min
        stats.Add(sensorLogs.Max(log => log.Value)); // Max
        stats.Add(sensorLogs.Average(log => log.Value)); // Average
        return stats;
    }
}