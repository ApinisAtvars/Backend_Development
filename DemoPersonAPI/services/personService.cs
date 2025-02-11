namespace DemoPersonAPI.Services;

public interface IDemoPersonAPIService
{
    void AddPerson(Person person);
    List<Person> GetPersons();
    Person GetPersonById(int id);
}
public class DemoPersonAPIService : IDemoPersonAPIService
{
    private readonly IPersonRepository _personRepository;
    private readonly PersonValidator _personValidator;
    private readonly IMailService _mailService;
    /*
    The app is compiled and executed.
    The dependency injection container is created.
    The app looks inside it to find the interface that is needed for this constructor.

    Constructor injection
    */
    public DemoPersonAPIService(IPersonRepository personRepository, PersonValidator personValidator, IMailService mailService)
    {
        _personRepository = personRepository;
        _personValidator = personValidator;
        _mailService = mailService;
    }

    public void AddPerson(Person person)
    {
        /*
        Better to have the validation here
        Imagine we use this service from an endpoint like now, but also that we use it somewhere else in our code.
        If the validation wasn't here, we would need to repeat ourselves in the other places too.
        */
        var validationResult = _personValidator.Validate(person);
        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors);
        }
        _personRepository.AddPerson(person);
        // Send an email
        _mailService.SendMail(person.Email, "YOU HAVE BEEN ADDED!", "as subject says");
    }

    public Person GetPersonById(int id) => _personRepository.GetPersonById(id);

    public List<Person> GetPersons() => _personRepository.GetPersons();
}