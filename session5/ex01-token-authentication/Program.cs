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