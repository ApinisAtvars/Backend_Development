namespace EnergySystem.Api.DTO
{
    public class SensorDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
    }

    public class SensorDTOProfile : Profile
    {
        public SensorDTOProfile()
        {
            CreateMap<Sensor, SensorDTO>();
            CreateMap<SensorDTO, Sensor>();
        }
    }
}