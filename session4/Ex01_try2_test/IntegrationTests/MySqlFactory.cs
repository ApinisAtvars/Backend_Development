
namespace Ex01_try2_test.Factories;

public class MySqlFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        string connectionString = "server=localhost;user=root;password=root;database=exercise1_test_v2";
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationContext>));
            services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


            var context = CreateContext(services);
            context.Database.EnsureDeleted();
            Thread.Sleep(2000);
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