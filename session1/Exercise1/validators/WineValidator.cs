namespace Exercise1.Validators;

public class WineValidator : AbstractValidator<Wine>
{
    public WineValidator()
    {
        RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name must be at least 5 characters");
        RuleFor(x => x.Year).GreaterThan(1900).LessThan(2022).WithMessage("Year must be between 1900 and 2022");    }
}