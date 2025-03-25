namespace Ex01_try2.Validators;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        // RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required");
        // RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required");
        // RuleFor(x => x.Plate).NotEmpty().WithMessage("Plate is required");
        
        //REGEX for plate
        RuleFor(x => x.Plate).Matches(@"^[A-Z]{3}-[0-9]{3}$|^[0-9]{1}-[A-Z]{3}-[0-9]{3}$").WithMessage("Plate is not valid");

        // RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required");
    }
}