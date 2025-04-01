namespace Sneakers.API.Validators;

public class SneakerValidator : AbstractValidator<NewSneakerDTO>
{
    public SneakerValidator()
    {
        RuleFor(sneaker => sneaker.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(sneaker => sneaker.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(sneaker => sneaker.Occasions).NotEmpty().WithMessage("At least one occasion is required");
        RuleFor(sneaker => sneaker.Brand).NotNull().WithMessage("Brand is required");
    }
}