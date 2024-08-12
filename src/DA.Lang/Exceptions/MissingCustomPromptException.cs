namespace DA.Lang.Exceptions;

public class MissingCustomPromptException : Exception
{
    public MissingCustomPromptException()
        : base("Custom tone must have a custom prompt.")
    {
    }
}