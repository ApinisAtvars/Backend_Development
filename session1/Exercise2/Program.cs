var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddTransient<BrandValidator>();
builder.Services.AddTransient<CarModelValidator>();
builder.Services.AddTransient<ICarModelRepository, CarModelRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICarModelService, CarModelService>();
builder.Services.AddTransient<IBrandService, BrandService>();

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/brands", (IBrandService brandService) =>
{
    try
    {
    var brands = brandService.GetBrands();
    return Results.Ok(brands);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapPost("/brands", (Brand brand, IBrandService brandService) =>
{
    try
    {
        brandService.AddBrand(brand);
        return Results.Created($"/brands/{brand.BrandId}", brand);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapGet("/brands/{id}", (int id, IBrandService brandService) =>
{
    try
    {
        var brand = brandService.GetBrandById(id);
        if (brand == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(brand);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapGet("brands/country/{country}", (string country, IBrandService brandService) =>
{
    try
    {
        var brands = brandService.GetBrandsFromCountry(country);
        return Results.Ok(brands);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapGet("/carmodels", (ICarModelService carModelService) =>
{
    try
    {
        var carModels = carModelService.GetCarModels();
        return Results.Ok(carModels);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapGet("/carmodels/{id}", (int id, ICarModelService carModelService) =>
{
    try
    {
        var carModel = carModelService.GetCarModelById(id);
        if (carModel == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(carModel);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapGet("/carmodels/brand/{brandId}", (int brandId, ICarModelService carModelService) =>
{
    try
    {
        var carModels = carModelService.GetCarModelsFromBrand(brandId);
        return Results.Ok(carModels);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapGet("/carmodels/country/{country}", (string country, ICarModelService carModelService) =>
{
    try
    {
        var carModels = carModelService.GetCarModelsFromCountry(country);
        return Results.Ok(carModels);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapPost("/carmodels", (CarModel carModel, ICarModelService carModelService) =>
{
    try
    {
        carModelService.AddCarModel(carModel);
        return Results.Created($"/carmodels/{carModel.CarModelId}", carModel);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.Run();
