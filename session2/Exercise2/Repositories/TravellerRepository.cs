namespace Exercise2.Repositories;

public interface ITravellerRepository
{
    Task<List<Traveller>> GetTravellers();
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
        return await _context.Travellers.ToListAsync();
    }
}