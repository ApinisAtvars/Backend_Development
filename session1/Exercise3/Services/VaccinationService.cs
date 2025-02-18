namespace Exercise3.Services;
public interface IVaccinationService
{
    VaccinationRegistration AddRegistration(VaccinationRegistration registration);
    List<VaccinationLocation> GetLocations();
    List<VaccinationRegistration> GetRegistrations();
    List<VaccineType> GetVaccins();
}

public class VaccinationService : IVaccinationService
{
    private readonly IVaccinationRegistrationRepository _vaccinationRegistrationRepository;
    private readonly IVaccinationLocationRepository _vaccinationLocationRepository;
    private readonly IVaccineTypeRepository _vaccineTypeRepository;
    private readonly VaccinationRegistrationValidator _vaccinationRegistrationValidator;
    public VaccinationService(IVaccinationRegistrationRepository vaccinationRegistrationRepository, IVaccinationLocationRepository vaccinationLocationRepository, IVaccineTypeRepository vaccineTypeRepository, VaccinationRegistrationValidator vaccinationRegistrationValidator)
    {
        _vaccinationRegistrationRepository = vaccinationRegistrationRepository;
        _vaccinationLocationRepository = vaccinationLocationRepository;
        _vaccineTypeRepository = vaccineTypeRepository;
        _vaccinationRegistrationValidator = vaccinationRegistrationValidator;
    }
    public VaccinationRegistration AddRegistration(VaccinationRegistration registration)
    {
        var validationResult = _vaccinationRegistrationValidator.Validate(registration);
        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors);
        }
        return _vaccinationRegistrationRepository.AddRegistration(registration);
    }

    public List<VaccinationLocation> GetLocations()
    {
        return _vaccinationLocationRepository.GetLocations();
    }

    public List<VaccinationRegistration> GetRegistrations()
    {
        return _vaccinationRegistrationRepository.GetVaccinationRegistrations();
    }

    public List<VaccineType> GetVaccins()
    {
        return _vaccineTypeRepository.GetVaccineTypes();
    }
}