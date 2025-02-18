namespace Exercise1.Repositories;

public interface IPersonRepository
{
    Task<List<Person>> GetPersons();
    Task<Person> GetPersonById(int id);
    Task AddPerson(Person person);
    Task UpdatePerson(Person person);
    Task DeletePerson(int id);
}

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationContext _context;

    public PersonRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Person>> GetPersons()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task<Person> GetPersonById(int id) => await _context.Persons.FindAsync(id);

    public async Task AddPerson(Person person)
    {
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePerson(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePerson(int id)
    {
        var person = await _context.Persons.FindAsync(id);
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
    }
}