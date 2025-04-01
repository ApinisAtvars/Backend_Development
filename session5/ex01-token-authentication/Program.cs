

// var builder = WebApplication.CreateBuilder(args);

// var configuration = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json")
//     .Build();


// builder.Services.Configure<DatabaseSettings>(configuration.GetSection("MongoConnection"));

// builder.Services.AddTransient<IMongoContext, MongoContext>();

// builder.Services.AddScoped<IMongoRepository, MongoRepository>();
// builder.Services.AddScoped<IMongoService, MongoService>();
// builder.Services.AddScoped<IUserRepository, UserRepository>();

// builder.Services.AddAutoMapper(typeof(Program));
// builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

// // NEW STUFF
// // This is not needed anymore
// // var apiKeySettings = builder.Configuration.GetSection("ApiKeySettings");
// // builder.Services.Configure<APIKeySettings>(apiKeySettings);

// var app = builder.Build();

// // app.UseMiddleware<ApiKeyMiddlewareDb>();








// // app.MapGet("/", () => "Hello World!");
// app.MapGet("/brands", async (IMongoService service) => await service.GetAllBrands());
// app.MapPost("/brands", async (IMongoService service, NewBrandDTO brand) => 
// {
//     await service.AddBrand(brand);
//     return Results.Created($"/brands/{brand.BrandId}", brand);
// });

// app.MapGet("/occasions", async (IMongoService service) => await service.GetAllOccasions());
// app.MapGet("/sneakers", async (IMongoService service, HttpContext context) => 
// {
//     try
//     {
//         // var customerNr = context.Items["CUSTOMERNR"].ToString(); // Get the customer number from the context
//         var allSneakers = await service.GetAllSneakers();
//         return Results.Ok(allSneakers);
//     }
//     catch (Exception ex)
//     {
//         return Results.InternalServerError(ex.Message);
//     }
// });

// app.MapGet("/seed", async (IMongoService service) => 
// {
//     await service.SetupData();
//     return Results.Ok("Data seeded");
// });

// app.MapPost("/sneakers", async (IMongoService service, NewSneakerDTO sneaker) => 
// {
//     try
//     {
//         await service.AddSneaker(sneaker);
//         return Results.Created($"/sneakers/{sneaker.SneakerId}", sneaker);
//     }
//     catch (FluentValidation.ValidationException ex)
//     {
//         return Results.BadRequest(ex.Message);
//     }
//     catch (Exception ex)
//     {
//         return Results.InternalServerError(ex.Message);
//     }

// });

// app.MapGet("/sneakers/{id}", async (IMongoService service, string id) => await service.GetSneaker(id));

// app.MapPost("/users", async (IUserRepository userRepository, User user) => 
// {
//     try{
//         var existingUser = await userRepository.GetUserByCustomerNr(user.CustomerNr);
//         if (existingUser != null)
//         {
//             return Results.Conflict("User with this customer number already exists.");
//         }
//         await userRepository.AddUser(user);
//         return Results.Created($"/users/{user.Id}", user);
//     }
//     catch (Exception ex)
//     {
//         return Results.InternalServerError(ex.Message);
//     }
// });

// app.MapGet("/users", async (IUserRepository userRepository) => await userRepository.GetUsers());


// app.Run();

// public partial class Program {}



var builder = WebApplication.CreateBuilder(args);
 
//Read the settings information from the configuration files
var settings = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(settings);

//Setup .NET 9 token security
builder.Services.AddAuthorizationBuilder();
builder.Services.AddAuthentication().AddBearerToken();

//Setup MySQL for users
var connectionString = settings.GetValue<string>("ConnectionStringUsers");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(connectionString)));
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<IMongoService, MongoService>();
builder.Services.AddValidatorsFromAssemblyContaining<SneakerValidator>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//Initialize the Authentication system for tokens
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>(); //Login, Register, ... endpoints


app.MapGet("/sneakers", async (IMongoService sneakerService, HttpContext context) =>
{
    return await sneakerService.GetAllSneakers();
}).RequireAuthorization();  //Make the API Secure

app.Run();