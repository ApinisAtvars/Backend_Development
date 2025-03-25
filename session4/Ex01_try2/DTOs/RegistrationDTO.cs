namespace Ex01_try2.DTOs;

public class RegistrationDTO
{
    public int RegistrationId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsFinished { get; set; }
    public int CarId { get; set; }
    public CarDTO Car { get; set; }

}

public class RegistrationProfileDTO : Profile
{
    public RegistrationProfileDTO()
    {
        CreateMap<Registration, RegistrationDTO>()
            .ForMember(dest => dest.RegistrationId, opt => opt.MapFrom(src => src.Id))
            // .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car.Id));
            .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car)); // Maps Car to CarDTO to prevent circular reference (I am very proud of this)
        CreateMap<RegistrationDTO, Registration>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RegistrationId))
            // .ForMember(dest => dest.Car.Id, opt => opt.MapFrom(src => src.CarId));
            .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car));
    }
}