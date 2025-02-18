namespace Exercise2.Services;

public interface IApplicationService
{
    Task<List<Traveller>> GetTravellers();
}
public class ApplicationService : IApplicationService
{
    private readonly ITravellerRepository _travellerRepository;

    public ApplicationService(ITravellerRepository travellerRepository)
    {
        _travellerRepository = travellerRepository;
    }

    public async Task<List<Traveller>> GetTravellers()
    {
        return await _travellerRepository.GetTravellers();
    }
}