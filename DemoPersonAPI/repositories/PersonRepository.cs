namespace DemoPersonAPI.Repositories;

public interface IPersonRepository
{
    void AddPerson(Person person);
    List<Person> GetPersons();
    Person GetPersonById(int id);
}

public class PersonRepository : IPersonRepository
{
    private static List<Person> _persons = new List<Person>();
    public void AddPerson(Person person)
    {
        person.Id = _persons.Count + 1;
        _persons.Add(person);
    }

    public Person GetPersonById(int id)
    {
        // SELECT * from Persons WHERE id = ID;
        return _persons.FirstOrDefault(x => x.Id == id);
    }

    public List<Person> GetPersons()
    {
        return _persons;
    }
}