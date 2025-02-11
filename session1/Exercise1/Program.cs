var builder = WebApplication.CreateBuilder(args);

// Inject the repos and services
builder.Services.AddTransient<IWineRepository, WineRepository>();
builder.Services.AddTransient<IWineService, WineService>();

var app = builder.Build();

app.MapGet("/wine", () =>
{
    return Results.Ok(new Wine() { Name = "Muller Kogl", Year = 2019, Country = "Austria", Color = "White", Price = 12.5m, Grapes = "Gruner Veltliner" });
});

// Add a wine via POST
app.MapPost("/wine", (Wine wine, IWineService wineService) =>
{
    wineService.AddWine(wine);
    return Results.Created($"/wine/{wine.WineId}", wine);
});

// Get all wines
app.MapGet("/wines", (IWineService wineService) =>
{
    return Results.Ok(wineService.GetWines());
});

app.MapDelete("/wine/{id}", (int id, IWineService wineService) =>
{
    try
    {
        wineService.DeleteWine(id);
        return Results.Ok();
    }
    catch (WineNotFoundException e)
    {
        return Results.NotFound(e.Message);
    }
    
});

app.Run("http://localhost:3000");
