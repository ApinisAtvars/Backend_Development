namespace Exercise1.Repositories;

public interface ITravellerRepository
{
    Task<List<Traveller>> GetTravellers();
    Task<Traveller> GetTravellerByID(int id);
    Task<List<Destination>> GetDestinations();
    Task<List<Guide>> GetGuides();
    Task<List<GuideTourDTO>> GetGuidesWithTours();
    Task AddTraveller(Traveller traveller);
    Task<Passport> GetPassportByNumber(string passportNumber);
    Task<Passport> AddPassport(Passport passport);
    Task<Guide> GetGuideByID(int id, bool includeTours = false);
    Task<Destination> AddDestination(Destination destination);
    Task<Traveller> AddTravellerToDestination(int travellerId, int destinationId);

}
public class TravellerRepository : ITravellerRepository
{
    private readonly ApplicationContext _context;
    public TravellerRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Traveller>> GetTravellers()
    {
        // return await _context.Travellers.ToListAsync();
        return await _context.Travellers
        .Include(t => t.Passport)  // Include passport information
        .ToListAsync();
    }

    public async Task<Traveller> GetTravellerByID(int id)
    {
        return await _context.Travellers
        .Include(t => t.Passport)  // Include passport information
        .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Destination>> GetDestinations()
    {
        return await _context.Destinations.ToListAsync();
    }

    public async Task<List<Guide>> GetGuides()
    {
        return await _context.Guides.ToListAsync();
    }

    public async Task<List<GuideTourDTO>> GetGuidesWithTours()
    {
        return await _context.Guides
        .Include(g => g.Tours)
        .Select(g => new GuideTourDTO
        {
            GuideId = g.Id,
            Name = g.Name,
            Tours = g.Tours.Select(t => new TourDTO
            {
                TourId = t.Id,
                Name = t.Title
            }).ToList()
        })
        .ToListAsync();
    }

    public async Task AddTraveller(Traveller traveller)
    {
        _context.Travellers.Add(traveller);
        await _context.SaveChangesAsync();
    }
    public async Task<Passport> GetPassportByNumber(string passportNumber)
    {
        return await _context.Passports.FirstOrDefaultAsync(p => p.PassportNumber == passportNumber);
    }
    // Should be run before adding Traveller, bcs there's no FK in this here table
    public async Task<Passport> AddPassport(Passport passport)
    {
        _context.Passports.Add(passport);
        await _context.SaveChangesAsync();
        // Really convoluted and stupid
        Passport result = await GetPassportByNumber(passport.PassportNumber);
        return result;
    }
    public async Task<Guide> GetGuideByID(int id, bool includeTours = false)
    {
        if (includeTours)
        {
            return await _context.Guides
                .Include(g => g.Tours)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        
        return await _context.Guides
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Destination> AddDestination(Destination destination)
    {
        _context.Destinations.Add(destination);
        await _context.SaveChangesAsync();
        await _context.Entry(destination).ReloadAsync();
        return destination;
    }

    // public async Task<Traveller> AddTravellerToDestination(int travellerId, int destinationId)
    // {
    //     Traveller traveller = await _context.Travellers.FirstOrDefaultAsync(t => t.Id == travellerId);
    //     Destination destination = await _context.Destinations.FirstOrDefaultAsync(d => d.Id == destinationId);
    //     traveller.Destinations.Add(destination);
    //     destination.Travellers.Add(traveller);
    //     await _context.SaveChangesAsync();
    //     return await _context.Travellers
    //         .Include(t => t.Destinations) // Include all the destinations
    //         .Include(t => t.Passport)  // Include passport information
    //         .FirstOrDefaultAsync(t => t.Id == travellerId);
    // }
    public async Task<Traveller> AddTravellerToDestination(int travellerId, int destinationId)
{
    Traveller traveller = await _context.Travellers.FirstOrDefaultAsync(t => t.Id == travellerId);
    Destination destination = await _context.Destinations.FirstOrDefaultAsync(d => d.Id == destinationId);
    
    if (traveller == null || destination == null)
    {
        throw new ArgumentException("Traveller or Destination not found");
    }
    
    // Initialize collections if they're null
    if (traveller.Destinations == null)
    {
        traveller.Destinations = new List<Destination>();
    }
    
    if (destination.Travellers == null)
    {
        destination.Travellers = new List<Traveller>();
    }
    
    traveller.Destinations.Add(destination);
    destination.Travellers.Add(traveller);
    
    await _context.SaveChangesAsync();
    
    return await _context.Travellers
        .Include(t => t.Destinations)
        .Include(t => t.Passport)
        .FirstOrDefaultAsync(t => t.Id == travellerId);
}
    
}