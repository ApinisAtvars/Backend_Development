

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.Configure<ApplicationSettings>(configuration.GetSection("MongoConnection"));

// Adding Context, Repositories and Services to the DI container
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IBrandService, BrandService>();

// Adding AutoMapper and FluentValidation to the DI container
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Add dummy data
app.MapGet("/setup", async (ICarService carService) =>
{
    await carService.SetupDummyData();
    return Results.Ok("Dummy data created");
});

// Get all cars
app.MapGet("/cars", async (ICarService carService, IMapper mapper) =>
{
    var cars = await carService.GetAllCars();
    return Results.Ok(mapper.Map<List<CarDTO>>(cars));
});

// Get a car by id
app.MapGet("/cars/{id}", async (ICarService carService, string id, IMapper mapper) =>
{
    var car = await carService.GetCar(id);
    return car != null ? Results.Ok(mapper.Map<CarDTO>(car)) : Results.NotFound();
});

// Get all brands
app.MapGet("/brands", async (IBrandService brandService, IMapper mapper) =>
{
    var brands = await brandService.GetAllBrands();
    return Results.Ok(mapper.Map<List<BrandDTO>>(brands));
});

// Get a brand by id
app.MapGet("/brands/{id}", async (IBrandService brandService, string id, IMapper mapper) =>
{
    var brand = await brandService.GetBrand(id);
    return brand != null ? Results.Ok(mapper.Map<BrandDTO>(brand)) : Results.NotFound();
});

// Add a new brand
app.MapPost("/brands", async (IBrandService brandService, Brand brand, IMapper mapper) =>
{
    try
    {
        var newBrand = await brandService.AddBrand(brand);
        return Results.Created($"/brands/{newBrand.Id}", mapper.Map<BrandDTO>(newBrand));
    }
    catch (FluentValidation.ValidationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.InternalServerError("Internal Server Error.");
    }
});

// Add a new car
app.MapPost("/cars", async (ICarService carService, Car car, IMapper mapper) =>
{
    try
    {
        var newCar = await carService.AddCar(car);
        return Results.Created($"/cars/{newCar.Id}", mapper.Map<CarDTO>(newCar));
    }
    catch (FluentValidation.ValidationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.InternalServerError("Internal Server Error.");
    }
});


app.Run();
