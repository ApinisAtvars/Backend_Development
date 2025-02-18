namespace Exercise3.Repositories;

public interface IVaccineTypeRepository
{
    List<VaccineType> GetVaccineTypes();
}

public class VaccineTypeRepository : IVaccineTypeRepository
{

    private static List<VaccineType> _vaccineTypes = new List<VaccineType>();

    public VaccineTypeRepository()
    {
        if (!(_vaccineTypes.Any()))
        {
            _vaccineTypes.Add(new VaccineType()
            {
                VaccinTypeId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
                Name = "Pfizer"
            });
            _vaccineTypes.Add(new VaccineType()
            {
                VaccinTypeId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d"),
                Name = "AstraZeneca"
            });
        }
    }

    public List<VaccineType> GetVaccineTypes()
    {
        return _vaccineTypes.ToList<VaccineType>();
    }
}