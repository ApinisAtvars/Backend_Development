namespace Exercise2.DTOs;

// public class TravellerDTO
// {
//     public int Id { get; set; }
//     public string FullName { get; set; }
//     public string PassportNumber { get; set; }
// }

// public class ProfileDTO : Profile
// {
//     public ProfileDTO()
//     {
//         // CreateMap<Traveller, TravellerDTO>()
//         //     .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.Passport.PassportNumber));

//     }
// }
public class TravellerDTO
{
    public int TravellerId { get; set; }  // Changed from Id
    public string FullName { get; set; }
    public string PassportNumber { get; set; }
}

public class ProfileDTO : Profile
{
    public ProfileDTO()
    {
        CreateMap<Traveller, TravellerDTO>()
            .ForMember(dest => dest.TravellerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.Passport.PassportNumber));
    }
}