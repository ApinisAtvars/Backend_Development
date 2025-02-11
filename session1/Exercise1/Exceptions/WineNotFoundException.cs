namespace Exercise1.Exceptions;

public class WineNotFoundException : Exception
{
    public WineNotFoundException(string message) : base(message)
    {

    }
}