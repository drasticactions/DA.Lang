namespace DA.Lang.Exceptions;

public class NeutralCultureException : Exception
{
    public NeutralCultureException()
        : base("Culture must not be neutral.")
    {
    }
}