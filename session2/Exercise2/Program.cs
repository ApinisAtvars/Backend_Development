var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the connection string from the appsettings.Development.json file.
/*
In the Dependency Injection container, create a new instance of the ApplicationContext class and register it as a service.
*/
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(typeof(Program));

// Slower at startup, but with one line of code you have covered all the validators
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<ITravellerRepository, TravellerRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

var app = builder.Build();

// This endpoint DOES NOT WORK like it should
// app.MapGet("/travellers", async (IApplicationService applicationService, IMapper mapper) =>
// {
//     var travellers = await applicationService.GetTravellers();    
//     var mappedTravellers = mapper.Map<List<TravellerDTO>>(travellers);
//     return Results.Ok(travellers);
// });

app.MapGet("/travellers", async (IApplicationService applicationService, IMapper mapper) =>
{
    var travellers = await applicationService.GetTravellers();    
    var mappedTravellers = mapper.Map<List<TravellerDTO>>(travellers);
    return Results.Ok(mappedTravellers);  // Return mapped DTOs instead of raw travellers
});


app.Run();
