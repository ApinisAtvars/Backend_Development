var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the connection string from the appsettings.Development.json file.
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IMySqlRepository, MySqlRepository>();
builder.Services.AddScoped<IMySqlService, MySqlService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.MapGet("/cars", async (IMySqlService mySqlService, IMapper mapper) =>
{
    List<Car> cars = await mySqlService.GetCars();
    var carsDTO = mapper.Map<List<CarDTO>>(cars);
    return Results.Ok(carsDTO);
});

app.MapPost("/cars", async (IMySqlService mySqlService, IMapper mapper, CarDTO carDTO) =>
{
    var car = mapper.Map<Car>(carDTO);
    car = await mySqlService.AddCar(car);
    var mappedResult = mapper.Map<CarDTO>(car);
    return Results.Created($"/cars/{mappedResult.CarId}", mappedResult);
});

app.Run();
