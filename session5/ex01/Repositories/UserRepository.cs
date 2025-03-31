namespace Sneakers.API.Repositories;

public interface IUserRepository
{
    Task<User> GetUserById(string id);
    Task<User> GetUserByCustomerNr(string customerNr);
    Task<User> GetUserByApiKey(string apiKey);
    Task AddUser(User user);
    Task<List<User>> GetUsers();
}

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;

    public UserRepository(IMongoContext context)
    {
        _userCollection = context.UsersCollection;
    }

    public async Task<User> GetUserById(string id)
    {
        return await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByCustomerNr(string customerNr)
    {
        return await _userCollection.Find(u => u.CustomerNr == customerNr).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByApiKey(string apiKey)
    {
        return await _userCollection.Find(u => u.ApiKey == apiKey).FirstOrDefaultAsync();
    }

    public async Task AddUser(User user)
    {
        await _userCollection.InsertOneAsync(user);
    }
    
    public async Task<List<User>> GetUsers()
    {
        return await _userCollection.Find(_ => true).ToListAsync();
    }
}