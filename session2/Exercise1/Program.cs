var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the connection string from the appsettings.Development.json file.
/*
In the Dependency Injection container, create a new instance of the ApplicationContext class and register it as a service.
*/
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(typeof(Program));

// Slower at startup, but with one line of code you have covered all the validators
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

// For this framework, we should use AddScoped
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

var app = builder.Build();

app.MapGet("/persons", async (IApplicationService applicationService,IMapper mapper) =>
{
    var persons = await applicationService.GetPersons();
    var mappedPersons = mapper.Map<List<PersonDTO>>(persons);
    return Results.Ok(mappedPersons);
});

app.MapGet("/persons/{id}", async (IApplicationService applicationService, int id, IMapper mapper) =>
{
    var person = await applicationService.GetPersonById(id);
    var mappedPerson = mapper.Map<PersonDTO>(person);
    return Results.Ok(mappedPerson);
});

app.MapPost("/persons", async (IApplicationService applicationService, Person person, IMapper mapper, IValidator<Person> validator) =>
{
    var validationResult = validator.Validate(person);
    // Doing the validation here means there's no need to create a custom exception
    // The only reason for doing the validation in the service layer is if you want to reuse the code for a mobile app
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }
    await applicationService.AddPerson(person);
    return Results.Created($"/persons/{person.Id}", mapper.Map<PersonDTO>(person));
});


app.Run();
