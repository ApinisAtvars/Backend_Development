namespace Sneakers.API.DTOs;

public class NewBrandDTO
{
    public string? BrandId { get; set; }
    public string Name { get; set; }
}

public class NewBrandProfile : Profile
{
    public NewBrandProfile()
    {
        CreateMap<NewBrandDTO, Brand>();
        CreateMap<Brand, NewBrandDTO>();
    }
}