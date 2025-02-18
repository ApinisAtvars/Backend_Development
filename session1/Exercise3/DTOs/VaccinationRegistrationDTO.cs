namespace Exercise3.DTOs;

public class VaccinRegistrationDTO
{
    public Guid VaccinatinRegistrationId { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? EMail { get; set; }
    public int YearOfBirth { get; set; }
    public string? VaccinationDate { get; set; }
    public string? VaccinName { get; set; }
    public string? Location { get; set; }
}

public class DTOProfile : Profile{
    public DTOProfile()
    {
        CreateMap<VaccinationRegistration, VaccinRegistrationDTO>()
        .ForMember(dest => dest.VaccinName , opt => opt.MapFrom<VaccinResolver>())
        .ForMember(dest => dest.Location , opt => opt.MapFrom<VaccinLocationResolver>());
    }
}

public class VaccinLocationResolver : IValueResolver<VaccinationRegistration, VaccinRegistrationDTO,string>{
    public string Resolve(VaccinationRegistration source, VaccinRegistrationDTO destination,string dest, ResolutionContext context)
    {
        List<VaccinationLocation> locations = context.Items["locations"] as List<VaccinationLocation>;
        return locations.Where(l => l.VaccinationLocationId == source.VaccinationLocationId).Single().Name;
    }
}

public class VaccinResolver : IValueResolver<VaccinationRegistration, VaccinRegistrationDTO,string>{
    public string Resolve(VaccinationRegistration source, VaccinRegistrationDTO destination,string dest, ResolutionContext context)
    {
        List<VaccineType> vaccins = context.Items["vaccins"] as List<VaccineType>;
        return vaccins.Where(l => l.VaccinTypeId == source.VaccinTypeId).Single().Name;
    }
}

