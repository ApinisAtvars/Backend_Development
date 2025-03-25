public class RegistrationDTO
{
    public int RegistrationId { get; set; }
    public string Plate { get; set; }
    public DateTime RegistrationStart { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public int CarId { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsFinished { get; set; }
}

public class RegistrationProfileDTO : Profile
{
    public RegistrationProfileDTO()
    {
        CreateMap<Registration, RegistrationDTO>()
            .ForMember(dest => dest.RegistrationId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RegistrationStart, opt => opt.MapFrom(src => src.Start))
            .ForMember(dest => dest.RegistrationEnd, opt => opt.MapFrom(src => src.End))
            .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car.Id));
    }
}