namespace Exercise1.DTOs;

public class PersonDTO
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class ProfileDTO : Profile
{
    public ProfileDTO()
    {
        CreateMap<Person, PersonDTO>();
    }
}