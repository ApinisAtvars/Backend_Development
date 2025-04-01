using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Sneakers.API.Configuration;


namespace Sneakers.API.Test.Factories;

public class SneakerApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MongoDbContainer mongoDbContainer = new MongoDbBuilder().WithImage("mongo:latest").Build();
    private MySqlContainer mySqlContainer = new MySqlBuilder().Build(); // Create a MySQL container

    public async Task InitializeAsync() // A method that starts the container
    {
        await mongoDbContainer.StartAsync();
        await mySqlContainer.StartAsync(); // Start the MySQL container
    }

    public async Task DisposeAsync() // A method that stops the container
    {
        await mongoDbContainer.DisposeAsync();
        await mySqlContainer.DisposeAsync(); // Stop the MySQL container
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(async services => {

            services.RemoveAll(typeof(IMongoContext)); // Plug out the current mongo context
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>)); // Plug out the current application context

            services.Configure<DatabaseSettings>(options => // Reconfigure the database settings
            {
                options.ConnectionString = mongoDbContainer.GetConnectionString(); // Get the connection string from the MongoDb container
                options.ConnectionStringUsers = mySqlContainer.GetConnectionString(); // Get the connection string from the MySQL container
                options.DatabaseName = "SneakersTestDb";
                options.SneakerCollection = "Sneakers";
                options.BrandsCollection = "Brands";
                options.OccasionCollection = "Occasions";
                options.OrdersCollection = "Orders";
                // options.UsersCollection = "Users";
                
            });

           var connectionString = mySqlContainer.GetConnectionString(); // Get the connection string from the MySQL container
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(connectionString)));
            
            

            services.AddScoped<IMongoContext>(sp => {
                // Read settings from DI container, and get IOptions<DatabaseSettings> object back
                var databaseSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>();
                // Pass these settings to the MongoContext constructor, which configures it to use the mongo db testcontainer that I defined earlier
                var mongoContext = new MongoContext(databaseSettings);
                return mongoContext;

            }); // Plug in the new mongo context

            var collections = new List<string>();
            collections.Add("Sneakers");
            collections.Add("Brands");
            collections.Add("Occasions");
            collections.Add("Orders");

            var diSystem = services.BuildServiceProvider(); // Get reference to the DI system
            var mongoContext = diSystem.GetRequiredService<IMongoContext>(); // Get the mongo context from the DI system

            foreach (var collection in collections)
            {
                // mongoContext.Database.DropCollection(collection); // Drop all collections
                // It's a clean database, so there's no need to drop the collections
                mongoContext.Database.CreateCollection(collection); // Recreate all collections
            }

            var brandsCollection = mongoContext.Database.GetCollection<Brand>("Brands");

            var dummyBrands = new List<Brand>
            {
                new Brand { BrandId = ObjectId.GenerateNewId().ToString(), Name = "Nike" },
                new Brand { BrandId = ObjectId.GenerateNewId().ToString(), Name = "Adidas" },
                new Brand { BrandId = ObjectId.GenerateNewId().ToString(), Name = "Puma" },
                new Brand { BrandId = ObjectId.GenerateNewId().ToString(), Name = "Rick Owens" },
                new Brand { BrandId = ObjectId.GenerateNewId().ToString(), Name = "Converse" }
            };

            brandsCollection.InsertMany(dummyBrands); // Insert dummy brands into the Brands collection

            var occasionsCollection = mongoContext.Database.GetCollection<Occasion>("Occasions");
            occasionsCollection.InsertOne(new Occasion { OccasionId = ObjectId.GenerateNewId().ToString(), Description = "Sports" });


            // Set up MySQL Test Container
            
            var context = CreateContext(services); // Create the application context
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            // Wait for database to be created
            // Thread.Sleep(2000); // Not needed, because we are using Testcontainers

            // Create a user
            var userManager = diSystem.GetRequiredService<UserManager<IdentityUser>>(); // Get the user manager from the DI system becaause we want to create a new user

            var dummyUser = new IdentityUser(){
                UserName = "testuser@test.com",
                Email = "testuser@test.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(dummyUser, "Test123!");
            Console.WriteLine($"User creation success: {result.Succeeded}");
        });
        // base.ConfigureWebHost(builder); // Call the base method to configure the web host
    }

    private static ApplicationDbContext CreateContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return context;
    }
}