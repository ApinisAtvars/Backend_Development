namespace Exercise1.Services;

public interface IApplicationService
{
    Task<List<Person>> GetPersons();
    Task<Person> GetPersonById(int id);
    Task AddPerson(Person person);
    Task<Person> UpdatePerson(Person person);
    Task DeletePerson(int id);
}
public class ApplicationService : IApplicationService
{
    private readonly IPersonRepository _personRepository;

    public ApplicationService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<List<Person>> GetPersons()
    {
        return await _personRepository.GetPersons();
    }

    public async Task<Person> GetPersonById(int id)
    {
        return await _personRepository.GetPersonById(id);
    }

    public async Task AddPerson(Person person)
    {
        await _personRepository.AddPerson(person);
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        await _personRepository.UpdatePerson(person);
        return person;
    }

    public async Task DeletePerson(int id)
    {
        await _personRepository.DeletePerson(id);
    }
}