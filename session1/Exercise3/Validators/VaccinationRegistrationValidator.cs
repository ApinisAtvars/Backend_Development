namespace Exercise3.Validators;

public class VaccinationRegistrationValidator : AbstractValidator<VaccinationRegistration>
{
    public VaccinationRegistrationValidator()
    {
        RuleFor(x => x.Name).                   NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.FirstName).              NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.EMail).                  NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.YearOfBirth).            NotEmpty().WithMessage("Year of birth is required");
        RuleFor(x => x.VaccinationDate).        NotEmpty().WithMessage("Vaccination date is required");
        RuleFor(x => x.VaccinTypeId).           NotEmpty().WithMessage("Vaccin type is required");
        RuleFor(x => x.VaccinationLocationId).  NotEmpty().WithMessage("Vaccination location is required");

        RuleFor(x=>x.YearOfBirth).              GreaterThan(1900).WithMessage("Year of birth must be greater than 1900");
        RuleFor(x=>x.YearOfBirth).              LessThan(DateTime.Now.Year).WithMessage("Year of birth must be less than current year");

        RuleFor(x=>x.EMail).                    EmailAddress().WithMessage("Email is not valid");
    }

}