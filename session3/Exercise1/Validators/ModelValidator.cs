namespace Exercise1.Validators;

public class DestinationValidator : AbstractValidator<Destination>
{
    public DestinationValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Name).MaximumLength(200).WithMessage("Name must be shorter than 200 characters.");
    }
}
//=======================================================================================
public class GuideValidator : AbstractValidator<Guide>
{
    public GuideValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Name).MaximumLength(200).WithMessage("Name must be shorter than 200 characters.");
    }
}
//=======================================================================================
public class PassportValidator : AbstractValidator<Passport>
{
    public PassportValidator()
    {
        RuleFor(x => x.PassportNumber).NotEmpty().WithMessage("Number is required.");
        RuleFor(x => x.PassportNumber).MaximumLength(20).WithMessage("Number must be shorter than 10 characters.");
    }
}
//=======================================================================================
public class TourValidator : AbstractValidator<Tour>
{
    public TourValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Title).MaximumLength(200).WithMessage("Title must be shorter than 200 characters.");
    }
}
//=======================================================================================
public class TravellerValidator : AbstractValidator<Traveller>
{
    public TravellerValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.FullName).MaximumLength(200).WithMessage("Name must be shorter than 200 characters.");
        RuleFor(x => x.Passport).NotNull().WithMessage("Passport is required.");
        
    }
}
//=======================================================================================