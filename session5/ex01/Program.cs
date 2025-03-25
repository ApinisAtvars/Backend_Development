var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.Configure<DatabaseSettings>(configuration.GetSection("MongoConnection"));

builder.Services.AddTransient<IMongoContext, MongoContext>();

builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<IMongoService, MongoService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// app.MapGet("/", () => "Hello World!");
app.MapGet("/brands", async (IMongoService service) => await service.GetAllBrands());
app.MapPost("/brands", async (IMongoService service, NewBrandDTO brand) => 
{
    await service.AddBrand(brand);
    return Results.Created($"/brands/{brand.BrandId}", brand);
});

app.MapGet("/occasions", async (IMongoService service) => await service.GetAllOccasions());
app.MapGet("/sneakers", async (IMongoService service) => await service.GetAllSneakers());

app.MapGet("/seed", async (IMongoService service) => 
{
    await service.SetupData();
    return Results.Ok("Data seeded");
});

app.MapPost("/sneakers", async (IMongoService service, NewSneakerDTO sneaker) => 
{
    try
    {
        await service.AddSneaker(sneaker);
        return Results.Created($"/sneakers/{sneaker.SneakerId}", sneaker);
    }
    catch (FluentValidation.ValidationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(ex.Message);
    }
    
});

app.Run();
