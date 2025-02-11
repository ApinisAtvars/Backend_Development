namespace DemoPersonAPI.Validators;

/*
Special class that checks if the rules here apply to the Person class in the Program.cs
We have to inherit from an AbstractValidator.
We also have to define the model that we want to validate
*/
public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        // In the parentheses is a lambda expression
        RuleFor(x => x.Age).GreaterThan(18).WithMessage("You must be over 18 years old");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 2 and 50 characters");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid");
    }
}