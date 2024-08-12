using System.Globalization;
using ConsoleAppFramework;
using DA.Lang.Models;

var app = ConsoleApp.Create();

app.Add<Commands>();

app.Run(args);

class Commands
{
    /// <summary>
    /// Translates the given text to the target language and tone.
    /// </summary>
    /// <param name="text">Text to translate.</param>
    /// <param name="lang">Language to translate to, IETF tag.</param>
    /// <param name="tone">Tone the translation should have.</param>
    /// <param name="apiToken">OpenAI API Token.</param>
    [Command("")]
    public async Task TranslateAsync(string text, string lang = "en", Tone tone = Tone.Neutral, string? apiToken = default)
    {
        apiToken = (string.IsNullOrEmpty(apiToken) ? Environment.GetEnvironmentVariable("OPEN_AI_KEY") : apiToken) ?? string.Empty;
        if (string.IsNullOrWhiteSpace(apiToken))
        {
            Console.WriteLine(DA.Lang.Translations.Common.ApiKeyMissing);
            return;
        }

        var cultureInfo = CultureInfo.GetCultureInfoByIetfLanguageTag(lang);
        if (cultureInfo.IsNeutralCulture)
        {
            return;
        }

        var translation = new DA.Lang.Services.OpenAITranslationService
        {
            ApiKey = apiToken,
            Tone = tone,
            Culture = cultureInfo
        };

        if (!translation.CanExecute)
        {
            return;
        }
    
        var result = await translation.TranslateAsync(text);
        Console.WriteLine(result?.TranslatedText);
    }
}