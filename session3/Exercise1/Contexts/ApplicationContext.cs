namespace Exercise1.Contexts;

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
        // Redundant to configure relationship from both sides

        // One-to-One relationship between Passport and Traveller
        modelBuilder.Entity<Passport>()
            .HasOne(p => p.Traveller)
            .WithOne(t => t.Passport)
            .HasForeignKey<Traveller>(t => t.PassportId)
            .IsRequired();

        // One-to-One relationship between Traveller and Passport
        // modelBuilder.Entity<Traveller>()
        //     .HasOne(t => t.Passport)
        //     .WithOne(p => p.Traveller)
        //     .HasForeignKey<Passport>(p => p.TravellerId)
        //     .IsRequired();

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
            new Traveller { Id = 1, FullName = "John Doe", PassportId = 1 },
            new Traveller { Id = 2, FullName = "Jane Doe", PassportId = 2 }
        );

        modelBuilder.Entity<Passport>().HasData(
            new Passport { Id = 1, PassportNumber = "123456"},
            new Passport { Id = 2, PassportNumber = "654321"}
        );

        modelBuilder.Entity<Destination>().HasData(
            new Destination { Id = 1, Name = "Paris" },
            new Destination { Id = 2, Name = "London" }
        );

        modelBuilder.Entity<Guide>().HasData(
            new Guide { Id = 1, Name = "Atvars Apenes"},
            new Guide { Id = 2, Name = "Violetta Viktoriia Nguyen"}
        );

        modelBuilder.Entity<Tour>().HasData(
            new Tour { Id = 1, Title = "Paris Tour", GuideId = 1 },
            new Tour { Id = 2, Title = "London Tour", GuideId = 2 },
            new Tour { Id = 3, Title = "Paris Tour", GuideId = 2 }
        );
    }
}