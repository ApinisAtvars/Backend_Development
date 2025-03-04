namespace Exercise1.DTOs;
public class TravellerDTO
{
    public int TravellerId { get; set; }  // Changed from Id
    public string FullName { get; set; }
    public string PassportNumber { get; set; }
}

public class DestinationDTO
{
    public int DestinationId { get; set; }  // Changed from Id
    public string Name { get; set; }
}

public class DestinationDTOV2
{
    public int DestinationId { get; set; }  // Changed from Id
    public string Name { get; set; }
    public int Price { get; set; } = 1000;
}

public class GuideDTO
{
    public int GuideId { get; set; }  // Changed from Id
    public string Name { get; set; }
}

public class GuideTourDTO
{
    public int GuideId { get; set; }
    public string Name { get; set; }
    public List<TourDTO> Tours { get; set; }
}

public class TravellerDestinationDTO
{
    public int TravellerId { get; set; }
    public string FullName { get; set; }
    public string PassportNumber { get; set; }
    public List<DestinationDTO> Destinations { get; set; }
}

public class TourDTO
{
    public int TourId { get; set; }
    public string Name { get; set; }
}

public class ProfileDTO : Profile
{
    public ProfileDTO()
    {
        CreateMap<Traveller, TravellerDTO>()
            .ForMember(dest => dest.TravellerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.Passport.PassportNumber));
    
        CreateMap<Destination, DestinationDTO>()
            .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<Guide, GuideDTO>()
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Guide, GuideTourDTO>()
            .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Tours, opt => opt.MapFrom(src => src.Tours));
        
        CreateMap<Traveller, TravellerDestinationDTO>()
            .ForMember(dest => dest.TravellerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.Passport.PassportNumber))
            .ForMember(dest => dest.Destinations, opt => opt.MapFrom(src => src.Destinations));
        
        CreateMap<Tour, TourDTO>()
            .ForMember(dest => dest.TourId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));

        CreateMap<Destination, DestinationDTOV2>()
            .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        
    }
}