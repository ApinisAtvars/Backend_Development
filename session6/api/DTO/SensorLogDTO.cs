
namespace EnergySystem.Api.DTO
{
    public class SensorLogDTO
    {
        public string BuildingId { get; set; }
        public string SensorId { get; set; }
        public float Value { get; set; }
        public string Unit { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class SensorLogProfile : Profile
    {
        public SensorLogProfile()
        {
            CreateMap<SensorLog, SensorLogDTO>();
            CreateMap<SensorLogDTO, SensorLog>();
        }
    }
}