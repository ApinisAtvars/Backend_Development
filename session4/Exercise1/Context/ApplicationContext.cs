namespace Exercise1.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define FK for Registration
        modelBuilder.Entity<Car>()
        .HasOne(c => c.Registration)
        .WithOne(r => r.Car)
        .HasForeignKey<Registration>(r => r.CarId);

        // Seed Data
        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Plate = "ABC123",
                Color = "Red"
            },
            new Car
            {
                Id = 2,
                Brand = "Honda",
                Model = "Civic",
                Plate = "XYZ987",
                Color = "Blue"
            }
        );

        modelBuilder.Entity<Registration>().HasData(
            new Registration
            {
                Id = 1,
                Plate = "ABC-123",
                Start = DateTime.Now,
                End = DateTime.Now.AddMonths(1),
                CarId = 1,
                TotalPrice = 100,
                IsFinished = false
            },
            new Registration
            {
                Id = 2,
                Plate = "XYZ-987",
                Start = DateTime.Now,
                End = DateTime.Now.AddMonths(1),
                CarId = 2,
                TotalPrice = 100,
                IsFinished = false
            }
        );
    }

    
}