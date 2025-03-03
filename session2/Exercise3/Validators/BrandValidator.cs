namespace Exercise3.Validators;

public class BrandValidator : AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Country).NotNull().NotEmpty().WithMessage("Country is required.");
    }
}