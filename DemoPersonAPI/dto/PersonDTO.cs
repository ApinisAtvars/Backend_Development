namespace DemoPersonAPI.DTO;

public class PersonDTO
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Profile is a class inside AutoMapper
public class PersonProfile : Profile
{
    public PersonProfile()
    {
        /*
        This code defines the mapping
        */
        CreateMap<Person, PersonDTO>();
    }
}