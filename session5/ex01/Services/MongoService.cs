namespace Sneakers.API.Services;

public interface IMongoService
{
    Task AddBrand(NewBrandDTO brand);
    Task<List<Brand>> GetAllBrands();
    Task<List<Occasion>> GetAllOccasions();
    Task AddSneaker(NewSneakerDTO newSneaker);
    Task<List<Sneaker>> GetAllSneakers();
    Task SetupData();
}

public class MongoService : IMongoService
{
    private readonly IMongoRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<NewSneakerDTO> _sneakerValidator;

    public MongoService(IMongoRepository repository, IMapper mapper, IValidator<NewSneakerDTO> sneakerValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _sneakerValidator = sneakerValidator;
    }

    public async Task AddBrand(NewBrandDTO brand)
    {
        Brand brandToAdd = _mapper.Map<Brand>(brand);
        await _repository.AddBrand(brandToAdd);
    }

    public async Task<List<Brand>> GetAllBrands()
    {
        return await _repository.GetAllBrands();
    }

    public async Task<List<Occasion>> GetAllOccasions()
    {
        return await _repository.GetAllOccasions();
    }

    public async Task AddSneaker(NewSneakerDTO newSneaker)
    {
        var validationResult = _sneakerValidator.Validate(newSneaker);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        Sneaker sneakerToAdd = _mapper.Map<Sneaker>(newSneaker);
        await _repository.AddSneaker(sneakerToAdd);
    }

    public async Task<List<Sneaker>> GetAllSneakers()
    {
        return await _repository.GetAllSneakers();
    }

        public async Task SetupData()
    {
        try
        {
            if (!(await _repository.GetAllBrands()).Any())
                await _repository.AddBrands(new List<Brand>() { new Brand() { Name = "ASICS" }, new Brand() { Name = "CONVERSE" }, new Brand() { Name = "JORDAN" }, new Brand() { Name = "PUMA" } });

            if (!(await _repository.GetAllOccasions()).Any())
                await _repository.AddOccasions(new List<Occasion>() { new Occasion() { Description = "Sports" }, new Occasion() { Description = "Casual" }, new Occasion() { Description = "Skate" }, new Occasion() { Description = "Diner" } });

            if (!(await _repository.GetAllSneakers()).Any())
            {
                var occasions = await _repository.GetAllOccasions();
                var brands = await _repository.GetAllBrands();
                await _repository.AddSneaker(new Sneaker() { Name = "GEL-KAYANO 14", Price = 150, Stock = 10, Brand = brands[0], Occasions = new List<Occasion>() { occasions[0], occasions[1] } });
                await _repository.AddSneaker(new Sneaker() { Name = "GEL-KAYANO 15", Price = 160, Stock = 10, Brand = brands[0], Occasions = new List<Occasion>() { occasions[0], occasions[1] } });
                await _repository.AddSneaker(new Sneaker() { Name = "GEL-KAYANO 16", Price = 170, Stock = 10, Brand = brands[0], Occasions = new List<Occasion>() { occasions[0], occasions[1] } });
                await _repository.AddSneaker(new Sneaker() { Name = "GEL-KAYANO 17", Price = 180, Stock = 10, Brand = brands[0], Occasions = new List<Occasion>() { occasions[0], occasions[1] } });
                await _repository.AddSneaker(new Sneaker() { Name = "GEL-KAYANO 18", Price = 190, Stock = 10, Brand = brands[0], Occasions = new List<Occasion>() { occasions[0], occasions[1] } });
                await _repository.AddSneaker(new Sneaker() { Name = "GEL-KAYANO 19", Price = 200, Stock = 10, Brand = brands[0], Occasions = new List<Occasion>() { occasions[0], occasions[1] } });
                await _repository.AddSneaker(new Sneaker() { Name = "GEL-KAYANO 20", Price = 210, Stock = 10, Brand = brands[0], Occasions = new List<Occasion>() { occasions[0], occasions[1] } });
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }


}