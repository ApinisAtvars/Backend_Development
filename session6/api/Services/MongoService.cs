namespace EnergySystem.Api.Services;

public interface IMongoService
{
    Task<string> AddBuilding(BuildingDTO building);
    Task<List<BuildingDTO>> GetBuildings();
    Task<BuildingDTO> GetBuilding(string id);
    Task AddSensorToBuilding(string buildingId, SensorDTO sensor);
    Task<string> AddSensorLog(SensorLogDTO sensorLog);
    Task<List<SensorLogDTO>> GetSensorLogByBuildingAndSensor(string buildingId, string sensorId);
    Task<List<SensorLogDTO>> GetSensorLogs();
    Task<List<float>> GetStatsForSensorLog(string buildingId, string sensorId);
}

public class MongoService : IMongoService
{
    private readonly IMongoRepository _repository;
    private readonly IMapper _mapper;
    private readonly BuildingNotFoundException _buildingNotFoundException;
    private readonly SensorNotFoundException _sensorNotFoundException;

    public MongoService(IMongoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> AddBuilding(BuildingDTO building)
    {
        var buildingToAdd = _mapper.Map<Building>(building);
        string buildingId = await _repository.AddBuilding(buildingToAdd);
        return buildingId;
    }

    public async Task<string> AddSensorLog(SensorLogDTO sensorLog)
    {
        var sensorLogToAdd = _mapper.Map<SensorLog>(sensorLog);
        return await _repository.AddSensorLog(sensorLogToAdd);
    }

    public async Task AddSensorToBuilding(string buildingId, SensorDTO sensor)
    {
        var building = await _repository.GetBuilding(buildingId);
        if (building == null)
        {
            throw new BuildingNotFoundException($"Building with ID {buildingId} not found.");
        }
        var sensorToAdd = _mapper.Map<Sensor>(sensor);
        await _repository.AddSensorToBuilding(buildingId, sensorToAdd);
    }

    public async Task<BuildingDTO> GetBuilding(string id)
    {
        var building = await _repository.GetBuilding(id);
        if (building == null)
        {
            throw new BuildingNotFoundException($"Building with ID {id} not found.");
        }
        return _mapper.Map<BuildingDTO>(building);
    }

    public async Task<List<BuildingDTO>> GetBuildings()
    {
        var buildings = await _repository.GetBuildings();
        if (buildings == null || !buildings.Any())
        {
            throw new BuildingNotFoundException("No buildings found.");
        }
        return _mapper.Map<List<BuildingDTO>>(buildings);
    }

    public async Task<List<SensorLogDTO>> GetSensorLogByBuildingAndSensor(string buildingId, string sensorId)
    {
        // First, check if the building exists
        var building = await _repository.GetBuilding(buildingId);
        if (building == null)
        {
            throw new BuildingNotFoundException($"Building with ID {buildingId} not found.");
        }
        // Then check if the sensor exists in the building
        var sensors = await _repository.GetSensorsForBuilding(buildingId);
        if (sensors == null || !sensors.Any(s => s.Id == sensorId))
        {
            throw new SensorNotFoundException($"Sensor with ID {sensorId} not found in building {buildingId}.");
        }
        // If both checks pass, retrieve the sensor logs
        var sensorLogs = await _repository.GetSensorLogByBuildingAndSensor(buildingId, sensorId);
        return _mapper.Map<List<SensorLogDTO>>(sensorLogs);
    }

    public async Task<List<SensorLogDTO>> GetSensorLogs()
    {
        var sensorLogs = await _repository.GetSensorLogs();
        if (sensorLogs == null || !sensorLogs.Any())
        {
            throw new SensorLogNotFoundException("No sensor logs found.");
        }
        return _mapper.Map<List<SensorLogDTO>>(sensorLogs);
    }
    public async Task<List<float>> GetStatsForSensorLog(string buildingId, string sensorId)
    {
        var sensorLogs = await GetSensorLogByBuildingAndSensor(buildingId, sensorId);
        if (sensorLogs == null || !sensorLogs.Any())
        {
            throw new SensorLogNotFoundException($"No sensor logs found for sensor {sensorId} in building {buildingId}.");
        }
        return await _repository.GetStatsForSensorLog(buildingId, sensorId);
    }
}