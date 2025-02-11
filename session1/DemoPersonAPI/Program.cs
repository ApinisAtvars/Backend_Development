var builder = WebApplication.CreateBuilder(args);

// ALWAYS needed for validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
/*
PersonValidator p = new PersonValidator(); 
Registers the PersonValidator class in a transient way
If some part of my app is asking for this class, this line will provide an instance of it.

Same with the other dependencies we've injected.
*/
builder.Services.AddTransient<PersonValidator>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<IDemoPersonAPIService, DemoPersonAPIService>();
builder.Services.AddTransient<IMailService, MailService>();

var app = builder.Build();

// Simplest api
app.MapGet("/hello", () => "Hello World!");

// Simple get with custom parameter
app.MapGet("/hello/{name}", (string name) => 
{
    return Results.BadRequest("No no no");
});

// Post with custom object, and validation
app.MapPost("/person", (Person person, IDemoPersonAPIService aPIService) => 
{
    try
    {
        aPIService.AddPerson(person);
        // If you are returning a Created, you have to define a location where someone can find the created resource
        return Results.Created($"person/{person.Id}", person);
    }
    catch (CustomValidationException e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapGet("/person", (IDemoPersonAPIService aPIService, IMapper mapper) => 
{
    var persons = aPIService.GetPersons();
    // Now the DTO mapping
    var results = mapper.Map<List<Person>, List<PersonDTO>>(persons);
    return Results.Ok(results);
});

app.MapGet("/person/{id}", (int id, IDemoPersonAPIService aPIService) => 
{
    var person = aPIService.GetPersonById(id);
    if (person == null)
    {
        return Results.BadRequest();
    }
    return Results.Ok(person);
});

app.Run();
