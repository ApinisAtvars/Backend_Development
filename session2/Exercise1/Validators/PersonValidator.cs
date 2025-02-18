namespace Exercise1.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name).   NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Age).    NotEmpty().WithMessage("Age is required");
        RuleFor(x => x.Email).  NotEmpty().WithMessage("Email is required");


        RuleFor(x => x.Age).    GreaterThanOrEqualTo(0).WithMessage("You must be over 0 years old");
        RuleFor(x => x.Age).    LessThan(120).          WithMessage("Person must be under 120 years old");
        RuleFor(x => x.Name).   MaximumLength(100).     WithMessage("Name must be shorter than 100 characters");
        RuleFor(x => x.Email).  EmailAddress().         WithMessage("Email must be valid");
    }
}