using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Ex01_try2_test.Factories;

public class MySqlFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public MySqlFactory()
    {
        InitializeAsync().GetAwaiter().GetResult();
    }
    private MySqlContainer mySqlContainer = new MySqlBuilder().WithImage("mysql:latest").Build();

    public async Task InitializeAsync()
    {
        await mySqlContainer.StartAsync();
        await Task.Delay(2000);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await mySqlContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // string connectionString = "server=localhost;user=root;password=root;database=exercise1_test_v2";

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationContext>));

            services.AddDbContext<ApplicationContext>(options => options.UseMySql(mySqlContainer.GetConnectionString(), ServerVersion.AutoDetect(mySqlContainer.GetConnectionString())));


            var context = CreateContext(services);
            // context.Database.EnsureDeleted();
            // Thread.Sleep(2000);
            context.Database.EnsureCreated();
            // Wait for database to be created
            Thread.Sleep(2000);
        });
    }

    private static ApplicationContext CreateContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        return context;
    }
}
// namespace Ex01_try2_test.Factories;

// public class MySqlFactory : WebApplicationFactory<Program>, IAsyncLifetime
// {
//     public MySqlFactory()
//     {
//         InitializeAsync().GetAwaiter().GetResult();
//     }

//     public async Task InitializeAsync()
//     {
//         await _mySqlContainer.StartAsync();
//     }

//     //private readonly string _connectionString = "server=localhost;user=root;password=root;database=parking_test";

//     private readonly MySqlContainer _mySqlContainer = new MySqlBuilder().Build();
//     protected override void ConfigureWebHost(IWebHostBuilder builder)
//     {

//         builder.ConfigureServices(services =>
//         {
//             services.RemoveAll<DbContextOptions<ApplicationContext>>(); // this plugs out the MySql connection from the application
//             services.AddDbContext<ApplicationContext>(options => options.UseMySql(_mySqlContainer.GetConnectionString(), ServerVersion.AutoDetect(_mySqlContainer.GetConnectionString())));

//             var context = CreateContext(services);
//             context.Database.EnsureDeleted();
//             context.Database.EnsureCreated();
//             //Wait for database to be created
//             Thread.Sleep(2000);
//         });

        
//         base.ConfigureWebHost(builder);
//     }

//     private static ApplicationContext CreateContext(IServiceCollection services)
//     {
//         var serviceProvider = services.BuildServiceProvider();
//         var scope = serviceProvider.CreateScope();
//         var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//         return context;
//     }

//     async Task IAsyncLifetime.DisposeAsync()
//     {
//         await _mySqlContainer.DisposeAsync();
        
//     }
// }