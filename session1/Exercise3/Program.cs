var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddTransient<IVaccinationRegistrationRepository, VaccinationRegistrationRepository>();
builder.Services.AddTransient<IVaccinationLocationRepository, VaccinationLocationRepository>();
builder.Services.AddTransient<IVaccineTypeRepository, VaccineTypeRepository>();
builder.Services.AddTransient<IVaccinationService, VaccinationService>();
builder.Services.AddTransient<VaccinationRegistrationValidator>();


var app = builder.Build();

app.MapGet("/locations",(IVaccinationService vaccinationService) =>{
    return Results.Ok(vaccinationService.GetLocations());
});

app.MapPost("/registration",(IVaccinationService vaccinationService, VaccinationRegistration registration) =>{
    try
    {

        vaccinationService.AddRegistration(registration);
        return Results.Created($"/registration/{registration.VaccinatinRegistrationId}", registration);
    }
    catch (CustomValidationException e)
    {
        return Results.BadRequest(e.Message);
    }
});

// app.MapGet("/registrations",(IVaccinationService vaccinationService) =>{
//     return Results.Ok(vaccinationService.GetRegistrations());
// });

app.MapGet("/vaccins",(IVaccinationService vaccinationService) =>{
    return Results.Ok(vaccinationService.GetVaccins());
});

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()
        ?.Error;
    if (exception is not null)
    {
        var response = new { error = exception.Message };
        context.Response.StatusCode = 400;

        await context.Response.WriteAsJsonAsync(response);
    }
}));

app.MapGet("/registrations", (IMapper mapper, IVaccinationService vaccinationService) =>
{
    var mapped = mapper.Map<List<VaccinRegistrationDTO>>(vaccinationService.GetRegistrations(),opts =>
    {
        opts.Items["locations"] = vaccinationService.GetLocations();
        opts.Items["vaccins"] = vaccinationService.GetVaccins();
    });
    return Results.Ok(mapped);
});



app.Run();
