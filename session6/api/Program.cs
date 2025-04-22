var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.Configure<DatabaseSettings>(configuration.GetSection("MongoConnection"));
builder.Services.AddTransient<IMongoContext, MongoContext>();

builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<IMongoService, MongoService>();

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

// Now using route groups

// app.MapGet("/buildings", async (IMongoService service) => await service.GetBuildings());

// app.MapPost("/buildings", async (IMongoService service, BuildingDTO building) => 
// {
//     await service.AddBuilding(building);
//     return Results.Created();
// });

app.MapGroup("/buildings")
    .BuildingGroup();

app.MapGroup("/sensorlogs")
    .SensorLogGroup();
app.Run();
