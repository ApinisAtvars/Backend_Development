var builder = WebApplication.CreateBuilder(args);

// Inject packages
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddOpenApi();

// Inject the repos and services
builder.Services.AddTransient<IWineRepository, WineRepository>();
builder.Services.AddTransient<IWineService, WineService>();
builder.Services.AddTransient<WineValidator>();

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/wine", () =>
{
    return Results.Ok(new Wine() { Name = "Muller Kogl", Year = 2019, Country = "Austria", Color = "White", Price = 12.5m, Grapes = "Gruner Veltliner" });
});

// Add a wine via POST
app.MapPost("/wine", (Wine wine, IWineService wineService) =>
{
    try
    {
        wineService.AddWine(wine);
        return Results.Created($"/wine/{wine.WineId}", wine);
    }
    catch (CustomValidationException e)
    {
        return Results.BadRequest(e.Message);
    }
    
});

// Get all wines
app.MapGet("/wines", (IWineService wineService) =>
{
    return Results.Ok(wineService.GetWines());
});

// Deleting a wine by id
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

// Editing a wine
app.MapPut("/wines", (Wine wine, IWineService wineService) =>
{
    try
    {
    var existingWine = wineService.GetWineById(wine.WineId);
    existingWine.Name = wine.Name;
    existingWine.Year = wine.Year;
    return Results.Ok(existingWine);
    }
    catch
    {
        return Results.NotFound();
    }
});
app.Run("http://localhost:3000");
