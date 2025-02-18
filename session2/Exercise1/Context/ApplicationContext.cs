namespace Exercise1.Contexts;

public class ApplicationContext : DbContext
{
    // The property name will be the name of the table in the database
    // It maps to a model called `Person` which is the model I created
    public DbSet<Person> Persons { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This is where you can configure the model
        // For example, you can set the primary key
        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, Name = "John Doe", Age = 30, Email = "john@gmail.com" },
            new Person { Id = 2, Name = "Jane Doe", Age = 25, Email = "jane@gmail.com" }
        );
    }
}