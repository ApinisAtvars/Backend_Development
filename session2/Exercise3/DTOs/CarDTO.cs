namespace Exercise3.DTOs;

public class CarDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Brand { get; set; }
    // public DateTime CreatedOn { get; set; }

}

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDTO>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name));
    }
}