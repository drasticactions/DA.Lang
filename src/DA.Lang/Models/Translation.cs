namespace DA.Lang.Models;

public class Translation
{
    public int Id { get; set; }
    
    public string? OriginalText { get; set; }
    
    public string? TranslatedText { get; set; }
    
    public Tone Tone { get; set; }
    
    public string? Language { get; set; }
    
    public string? TranslationService { get; set; }
    
    public DateTime TranslatedAt { get; set; }
}