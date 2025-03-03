namespace Exercise3.DTOs;

public class BrandDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    // public DateTime CreatedOn { get; set; }

}

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandDTO>();
    }
}