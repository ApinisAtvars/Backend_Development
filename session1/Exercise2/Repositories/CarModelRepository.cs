namespace Exercise2.Repositories;

public interface ICarModelRepository
{
    List<CarModel> GetCarModels();
    List<CarModel> GetCarModelsFromBrand(Brand brand); // I hope C# is not the same as Python
    CarModel GetCarModelById(int id);
    List<CarModel> GetCarModelsFromCountry(string country);
    void AddCarModel(CarModel carModel);
}

public class CarModelRepository : ICarModelRepository
{
    private static List<CarModel> _carModels = new List<CarModel>();
    public CarModel GetCarModelById(int id)
    {
        return _carModels.FirstOrDefault(x => x.CarModelId == id);
    }

    public List<CarModel> GetCarModels()
    {
        return _carModels;
    }

    public List<CarModel> GetCarModelsFromBrand(Brand brand)
    {
        return _carModels.Where(x => x.Brand == brand).ToList();
    }

    public List<CarModel> GetCarModelsFromCountry(string country)
    {
        return _carModels.Where(x => x.Brand.Country == country).ToList();
    }

    public void AddCarModel(CarModel carModel)
    {
        // carModel.CarModelId = _carModels.Count + 1;
        _carModels.Add(carModel);
    }
}