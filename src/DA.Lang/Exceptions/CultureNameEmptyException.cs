namespace DA.Lang.Exceptions;

public class CultureNameEmptyException : Exception
{
    public CultureNameEmptyException()
        : base("Culture name must not be empty.")
    {
    }
}