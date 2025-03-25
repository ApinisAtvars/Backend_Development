namespace Ex01_try2.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Registration>()
            .HasOne(r => r.Car)
            .WithOne(c => c.Registration)
            .HasForeignKey<Registration>(r => r.CarId);

        // Seed Data
        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Plate = "ABC-123",
                Color = "Red"
            },
            new Car
            {
                Id = 2,
                Brand = "Honda",
                Model = "Civic",
                Plate = "XYZ-987",
                Color = "Blue"
            },
            new Car // One Extra Car that doesn't have a registration assigned to it, for integration tests
            {
                Id = 3,
                Brand = "Suzuki",
                Model = "Swift",
                Plate = "DEF-456",
                Color = "Green"
            }
        );

        modelBuilder.Entity<Registration>().HasData(
            new Registration
            {
                Id = 1,
                Start = DateTime.Now,
                End = DateTime.Now.AddMonths(1),
                CarId = 1,
                TotalPrice = 100,
                IsFinished = false
            },
            new Registration
            {
                Id = 2,
                Start = DateTime.Now,
                End = DateTime.Now.AddMonths(1),
                CarId = 2,
                TotalPrice = 100,
                IsFinished = false
            }
        );
    }

    
}