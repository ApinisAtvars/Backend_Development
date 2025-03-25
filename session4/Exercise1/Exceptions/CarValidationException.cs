
namespace Exercise1.Exceptions;

public class CarValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; set; }

    public CarValidationException(IEnumerable<ValidationFailure> errors) : base("Car Validation Failed :(")
    {
        Errors = errors;
    }
    
}