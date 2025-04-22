namespace EnergySystem.Api.DTO;

public class BuildingDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public List<SensorDTO> Sensors { get; set; }

    
}

public class BuildingDTOProfile : Profile
{
    public BuildingDTOProfile()
    {
        CreateMap<Building, BuildingDTO>();
        CreateMap<BuildingDTO, Building>();
    }
}