namespace Exercise3.Repositories;

public interface IVaccinationRegistrationRepository
{
    List<VaccinationRegistration> GetVaccinationRegistrations();
    VaccinationRegistration AddRegistration(VaccinationRegistration registration);
}

public class VaccinationRegistrationRepository : IVaccinationRegistrationRepository
{

    private static List<VaccinationRegistration> _registrations = new List<VaccinationRegistration>();

    public VaccinationRegistrationRepository()
    {
        if (!(_registrations.Any()))
        {
            _registrations.Add(new VaccinationRegistration()
            {
                VaccinatinRegistrationId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
                VaccinationLocationId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
                VaccinTypeId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
            });
            _registrations.Add(new VaccinationRegistration()
            {
                VaccinatinRegistrationId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d"),
                VaccinationLocationId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d"),
                VaccinTypeId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d"),
            });
        }
    }

    public List<VaccinationRegistration> GetVaccinationRegistrations()
    {
        return _registrations.ToList<VaccinationRegistration>();
    }

    public VaccinationRegistration AddRegistration(VaccinationRegistration registration)
    {
        registration.VaccinatinRegistrationId = Guid.NewGuid();
        _registrations.Add(registration);
        return registration;

    }
}