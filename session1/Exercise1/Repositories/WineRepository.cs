namespace Exercise1.Repositories;

public interface IWineRepository
{
    void AddWine(Wine wine);
    List<Wine> GetWines();
    Wine GetWineById(int id);
    void DeleteWine(Wine wine);
}

public class WineRepository : IWineRepository
{
    private static List<Wine> _wines = new List<Wine>();
    public void AddWine(Wine wine)
    {
        wine.WineId = _wines.Count + 1;
        _wines.Add(wine);
    }

    public Wine GetWineById(int id)
    {
        // SELECT * from Wines WHERE id = ID;
        try
        {
        return _wines.FirstOrDefault(x => x.WineId == id);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<Wine> GetWines()
    {
        return _wines;
    }

    public void DeleteWine(Wine wine)
    {
        _wines.Remove(wine);
    }
}