namespace Exercise3.Validators;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Brand).NotNull().NotEmpty().WithMessage("Brand is required.");
    }
}