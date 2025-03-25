namespace Exercise1.Exceptions;

public class RegistrationValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; set; }

    public RegistrationValidationException(IEnumerable<ValidationFailure> errors) : base("Registration Validation Failed :(")
    {
        Errors = errors;
    }
}