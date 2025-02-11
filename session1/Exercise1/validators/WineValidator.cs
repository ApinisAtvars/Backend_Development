namespace Exercise1.Validators;

public class WineValidator : AbstractValidator<Wine>
{
    public WineValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Year).NotEmpty().WithMessage("Year is required");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.Grapes).NotEmpty().WithMessage("Grapes is required");

        RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 2 and 50 characters");
        RuleFor(x => x.Year).GreaterThan(0).WithMessage("Year must be greater than 0");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}