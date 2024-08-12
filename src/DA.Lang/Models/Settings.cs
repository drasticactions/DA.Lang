namespace DA.Lang.Models;

public class Settings
{
    public int Id { get; set; }

    public string OpenAiKey { get; set; } = string.Empty;
    
    public string DefaultOutputLanguage { get; set; } = "ja-JP";
}