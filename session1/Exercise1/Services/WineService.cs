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
    private readonly WineValidator _wineValidator;
    public WineService(IWineRepository wineRepository, WineValidator wineValidator)
    {
        _wineRepository = wineRepository;
        _wineValidator = wineValidator;
    }

    public void AddWine(Wine wine)
    {
        var validationResult = _wineValidator.Validate(wine);
        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors);
        }
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
        try
        {
        _wineRepository.DeleteWine(wine);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}