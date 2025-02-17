namespace Exercise2.Validators;

public class CarModelValidator : AbstractValidator<CarModel>
{
    public CarModelValidator()
    {
        RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name must be at least 5 characters");
    }
}