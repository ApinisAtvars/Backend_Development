namespace Exercise1.DTOs;

public class CarDTO
{
    public int CarId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
    public string Color { get; set; }
    public List<int> RegistrationId { get; set; }
}

public class CarProfileDTO : Profile
{
    public CarProfileDTO()
    {
        CreateMap<Car, CarDTO>()
            .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RegistrationId, opt => opt.MapFrom(src => src.Registration.Id));
    }
}