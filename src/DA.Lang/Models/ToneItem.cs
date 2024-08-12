namespace DA.Lang.Models;

public class ToneItem(Tone tone)
{
    public Tone Tone => tone;
    
    public string FriendlyString => tone.ToFriendlyString();
    
    public static IEnumerable<ToneItem> GetTones()
    {
        return Enum.GetValues<Tone>().Select(tone => new ToneItem(tone));
    }
}