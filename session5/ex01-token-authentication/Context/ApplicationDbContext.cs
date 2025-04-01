namespace Sneakers.API.Context;

public class ApplicationDbContext : IdentityDbContext{

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
}
