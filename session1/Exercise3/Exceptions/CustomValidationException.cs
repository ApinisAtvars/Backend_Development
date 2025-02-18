namespace Exercise3.Exceptions;

public class CustomValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; set;}

    public CustomValidationException(IEnumerable<ValidationFailure> errors) : base("Validation failed")
    {
        Errors = errors;
    }
}