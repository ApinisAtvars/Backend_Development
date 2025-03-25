var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the connection string from the appsettings.Development.json file.
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IMySqlRepository, MySqlRepository>();
builder.Services.AddScoped<IMySqlService, MySqlService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.MapGet("/cars", async (IMySqlService mySqlService) =>
{
    List<CarDTO> carsDTO = await mySqlService.GetCars();
    return Results.Ok(carsDTO);
});

app.MapPost("/cars", async (IMySqlService mySqlService, IMapper mapper, CarDTO carDTO) =>
{ 
    try
    {
        carDTO = await mySqlService.AddCar(carDTO);
        return Results.Created($"/cars/{carDTO.CarId}", carDTO);
    }
    catch (ArgumentException e)
    {
        return Results.BadRequest(e.Message);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e.Message);
    }
    
});

app.MapGet("/registrations", async (IMySqlService mySqlService) =>
{
    List<RegistrationDTO> registrationsDTO = await mySqlService.GetRegistrations();
    return Results.Ok(registrationsDTO);
});

app.MapPost("/registrations", async (IMySqlService mySqlService, RegistrationDTO registration) =>
{
    try
    {
        RegistrationDTO registrationDTO = await mySqlService.AddRegistration(registration);
        return Results.Created($"/registrations/{registrationDTO.RegistrationId}", registrationDTO);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e.Message);
    }
});

app.MapPut("/registrations/{registrationId}", async (IMySqlService mySqlService, int registrationId) =>
{
    try
    {
        RegistrationDTO registrationDTO = await mySqlService.PutRegistration(registrationId);
        return Results.Ok(registrationDTO);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e.Message);
    }
});

app.Run();

public partial class Program ();