namespace Exercise1.Services;

public interface IWineService
{
    void AddWine(Wine wine);
    List<Wine> GetWines();
    Wine GetWineById(int id);
    void DeleteWine(int wineId);
}

public class WineService : IWineService
{
    private readonly IWineRepository _wineRepository;
    public WineService(IWineRepository wineRepository)
    {
        _wineRepository = wineRepository;
    }

    public void AddWine(Wine wine)
    {
        _wineRepository.AddWine(wine);
    }

    public Wine GetWineById(int id)
    {
        return _wineRepository.GetWineById(id);
    }

    public List<Wine> GetWines()
    {
        return _wineRepository.GetWines();
    }

    public void DeleteWine(int wineId)
    {
        Wine wine = _wineRepository.GetWineById(wineId);
        if (wine == null)
        {
            throw new WineNotFoundException("Wine not found");
        }
        _wineRepository.DeleteWine(wine);
    }
}