namespace Exercise2.DTOs;

public class TravellerDTO
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PassportNumber { get; set; }
}

public class ProfileDTO : Profile
{
    public ProfileDTO()
    {
        CreateMap<Traveller, TravellerDTO>()
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.Passport.PassportNumber));
    }
}