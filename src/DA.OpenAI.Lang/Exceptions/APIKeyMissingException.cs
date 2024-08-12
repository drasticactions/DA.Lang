namespace DA.OpenAI.Lang.Exceptions;

public class APIKeyMissingException : Exception
{
    public APIKeyMissingException()
        : base("API key must not be empty.")
    {
    }
}