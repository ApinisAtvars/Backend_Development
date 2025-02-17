
namespace Exercise2.Validators;

public class BrandValidator : AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name must be at least 5 characters");
        RuleFor(x => x.Country).MinimumLength(5).WithMessage("Country must be at least 5 characters");
        RuleFor(x => x.Logo).MinimumLength(5).WithMessage("Logo must be at least 5 characters");
    }
}