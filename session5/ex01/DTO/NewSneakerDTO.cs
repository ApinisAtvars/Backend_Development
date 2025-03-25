namespace Sneakers.API.DTOs;

public class NewSneakerDTO
{
    public string? SneakerId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Brand Brand { get; set; }
    public List<Occasion> Occasions { get; set; }
}

public class NewSneakerProfile : Profile
{
    public NewSneakerProfile()
    {
        CreateMap<NewSneakerDTO, Sneaker>();
        CreateMap<Sneaker, NewSneakerDTO>();
    }
}