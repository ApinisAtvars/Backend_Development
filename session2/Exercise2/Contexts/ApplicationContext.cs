namespace Exercise2.Contexts;

public class ApplicationContext : DbContext
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Guide> Guides { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<Traveller> Travellers { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-One relationship between Traveller and Passport
        modelBuilder.Entity<Traveller>()
            .HasOne(t => t.Passport)
            .WithOne(p => p.Traveller)
            .HasForeignKey<Passport>(p => p.TravellerId)
            .IsRequired();
        // One-to-Many relationship between Guide and Tour
        modelBuilder.Entity<Guide>()
            .HasMany(g => g.Tours)
            .WithOne(t => t.Guide)
            .HasForeignKey(t => t.GuideId)
            .IsRequired();
        // Many-to-Many relationship between Traveller and Destination
        modelBuilder.Entity<Traveller>()
            .HasMany(t => t.Destinations)
            .WithMany(d => d.Travellers)
            .UsingEntity<TravellerDestination>();

        // Seed data
        modelBuilder.Entity<Traveller>().HasData(
            new Traveller { Id = 1, FullName = "John Doe" },
            new Traveller { Id = 2, FullName = "Jane Doe" }
        );

        modelBuilder.Entity<Passport>().HasData(
            new Passport { Id = 1, PassportNumber = "123456", TravellerId = 1 },
            new Passport { Id = 2, PassportNumber = "654321", TravellerId = 2 }
        );
    }
}