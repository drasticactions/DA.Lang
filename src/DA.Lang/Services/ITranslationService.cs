using System.Globalization;
using DA.Lang.Exceptions;
using DA.Lang.Models;

namespace DA.Lang.Services;

public interface ITranslationService
{
    public Tone Tone { get; set; }
    
    public CultureInfo Culture { get; set; }
    
    public string CustomTone { get; set; }

    public string TranslationService { get; }

    public event EventHandler? RaiseOnExecuteChanged;
    
    public bool CanExecute { get; }
    
    public Task<Translation?> TranslateAsync(string text);
}

public static class TranslationServiceExtensions
{
    public static string GenerateSystemMessage(this ITranslationService service)
    {
        var tone = service.Tone.ToPromptString();
        if (service.Tone == Tone.Custom)
        {
            if (string.IsNullOrWhiteSpace(service.CustomTone))
            {
                throw new MissingCustomPromptException();
            }
            tone = service.CustomTone;
        }

        if (service.Culture.IsNeutralCulture)
        {
            throw new NeutralCultureException();
        }
        
        if (tone.EndsWith('!') || tone.EndsWith('?'))
        {
            tone = tone.Substring(0, tone.Length - 1);
        }
        
        if (!tone.EndsWith('.'))
        {
            tone += ".";
        }
        
        var cultureName = service.Culture.EnglishName;
        if (string.IsNullOrEmpty(cultureName))
        {
            throw new CultureNameEmptyException();
        }

        return $"You are a translator that will translate the following dialog into {cultureName}. {tone} You will translate the text as natural as you can, such as translating idioms.";
    }
}