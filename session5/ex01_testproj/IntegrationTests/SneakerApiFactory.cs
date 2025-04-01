using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Sneakers.API.Configuration;

namespace Sneakers.API.Test.Factories;

public class SneakerApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MongoDbContainer mongoDbContainer = new MongoDbBuilder().WithImage("mongo:latest").Build();

    public async Task InitializeAsync() // A method that starts the container
    {
        await mongoDbContainer.StartAsync();
    }

    public async Task DisposeAsync() // A method that stops the container
    {
        await mongoDbContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => {
            services.RemoveAll(typeof(IMongoContext)); // Plug out the current mongo context

            services.Configure<DatabaseSettings>(options => // Reconfigure the database settings
            {
                options.ConnectionString = mongoDbContainer.GetConnectionString(); // Get the connection string from the MongoDb container
                options.DatabaseName = "SneakersTestDb";
                options.SneakerCollection = "Sneakers";
                options.BrandsCollection = "Brands";
                options.OccasionCollection = "Occasions";
                options.OrdersCollection = "Orders";
                options.UsersCollection = "Users";
                
            });

            // Hardcode API key
            // Commented out because now we have different API keys for different users
            // services.Configure<APIKeySettings>(options => {
            //     options.ApiKey = "secret key";
            // });

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
            collections.Add("Users");

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

            var userCollection = mongoContext.Database.GetCollection<User>("Users");
            var dummyUser = new User(){
                Id = ObjectId.GenerateNewId().ToString(),
                CustomerNr = "K0001",
                Discount = 4,
                ApiKey = "usersecret"
            };
            userCollection.InsertOne(dummyUser); // Insert dummy user into the Users collection, this  user we will use for testing
        });
    }


}