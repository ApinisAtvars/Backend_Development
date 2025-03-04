
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
// Serilog configuration		
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug()
    .CreateLogger();
// Register Serilog
builder.Logging.AddSerilog(logger);



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

builder.Services.AddMemoryCache();




/*
Adding Versioning to the API
*/
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
});

var app = builder.Build();



var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .HasApiVersion(new ApiVersion(2, 0)).Build();


app.MapGroup("/travellers").WithApiVersionSet(versionSet).GroupTravellers();

app.MapGroup("/guides").GroupGuides();

app.MapGroup("/destinations").GroupDestinationsVersion1().WithApiVersionSet(versionSet).MapToApiVersion(1.0);

app.MapGroup("/destinations").GroupDestinationsVersion2().WithApiVersionSet(versionSet).MapToApiVersion(2.0);

app.MapPost("/upload", (IFormFile file) => {
    using var reader = new StreamReader(file.OpenReadStream());
    var content = reader.ReadToEnd();
    var fileToWirte = File.OpenWrite($"./Uploads/{file.FileName}");
    using (var writer = new StreamWriter(fileToWirte))
    {
        writer.Write(content);
    }
    return Results.Ok("File downloaded in Uploads folder!");}).DisableAntiforgery();

app.Run();
