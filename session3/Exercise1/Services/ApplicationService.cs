namespace Exercise1.Services;

public interface IApplicationService
{
    Task<List<Traveller>> GetTravellers();
    Task<Traveller> GetTravellerByID(int id);
    Task<List<Destination>> GetDestinations();
    Task<List<Guide>> GetGuides();
    Task<List<GuideTourDTO>> GetGuidesWithTours();
    Task AddTraveller(string fullName, string passportNumber);
    Task<Guide> GetGuideByID(int id, bool includeTours = false);
    Task<Destination> AddDestination(Destination destination);
    Task<Traveller> AddTravellerToDestination(int travellerId, int destinationId);
}

public class ApplicationService : IApplicationService
{
    private readonly ITravellerRepository _travellerRepository;
    private IMemoryCache _memoryCache;
    private ILoggerFactory _loggerFactory;

    public ApplicationService(ITravellerRepository travellerRepository, IMemoryCache memoryCache, ILoggerFactory loggerFactory)
    {
        _travellerRepository = travellerRepository;
        _memoryCache = memoryCache;
        _loggerFactory = loggerFactory;
    }

    public async Task<List<Traveller>> GetTravellers()
    {
        return await _memoryCache.GetOrCreateAsync("travellers", async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromSeconds(10);
            var logger = _loggerFactory.CreateLogger("Travellers");
            try{
                logger.LogInformation("Travellers retrieved successfully");
                return await _travellerRepository.GetTravellers();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured");
                throw ex;
            }
        });
        // return await _travellerRepository.GetTravellers();
    }

    public async Task<Traveller> GetTravellerByID(int id)
    {
        return await _travellerRepository.GetTravellerByID(id);
    }

    public async Task<List<Destination>> GetDestinations()
    {
        return await _memoryCache.GetOrCreateAsync("destinations", async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromSeconds(10);
            return await _travellerRepository.GetDestinations();
        });
        // return await _travellerRepository.GetDestinations();
    }

    public async Task<List<Guide>> GetGuides()
    {
        return await _travellerRepository.GetGuides();
    }
    public async Task<List<GuideTourDTO>> GetGuidesWithTours()
    {
        return await _travellerRepository.GetGuidesWithTours();
    }

    public async Task AddTraveller(string fullName, string passportNumber)
    {
        if (await _travellerRepository.GetPassportByNumber(passportNumber) != null)
        {
                throw new Exception("Traveller with this passport number already exists");
        }
        
        var passportToAdd = new Passport { PassportNumber = passportNumber };
        Passport newPassport = await _travellerRepository.AddPassport(passportToAdd);
        var traveller = new Traveller { FullName = fullName, Passport = newPassport };
        await _travellerRepository.AddTraveller(traveller);
    }

    public async Task<Guide> GetGuideByID(int id, bool includeTours = false)
    {
        return await _travellerRepository.GetGuideByID(id, includeTours);
    }
    public async Task<Destination> AddDestination(Destination destination)
    {
        return await _travellerRepository.AddDestination(destination);
    }

    public async Task<Traveller> AddTravellerToDestination(int travellerId, int destinationId)
    {
        return await _travellerRepository.AddTravellerToDestination(travellerId, destinationId);
    }
}