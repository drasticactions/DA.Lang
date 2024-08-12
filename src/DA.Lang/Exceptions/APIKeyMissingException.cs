namespace DA.Lang.Exceptions;

public class APIKeyMissingException : Exception
{
    public APIKeyMissingException()
        : base("API key must not be empty.")
    {
    }
}