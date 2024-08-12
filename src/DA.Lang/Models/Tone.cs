namespace DA.Lang.Models;

public enum Tone
{
    Neutral,
    Custom,
    Casual,
    Formal,
    Professional,
    Honorific,
    Humble,
    Friendly,
    Sarcastic,
    Angry,
    SocialMedia,
    Texting,
}

public static class ToneExtensions
{
    public static string ToFriendlyString(this Tone tone)
    {
        return tone switch
        {
            Tone.Neutral => DA.Lang.Translations.Common.Neutral,
            Tone.Custom => DA.Lang.Translations.Common.Custom,
            Tone.Casual => DA.Lang.Translations.Common.Casual,
            Tone.Formal => DA.Lang.Translations.Common.Formal,
            Tone.Professional => DA.Lang.Translations.Common.Professional,
            Tone.Honorific => DA.Lang.Translations.Common.Honorific,
            Tone.Humble => DA.Lang.Translations.Common.Humble,
            Tone.Friendly => DA.Lang.Translations.Common.Friendly,
            Tone.Sarcastic => DA.Lang.Translations.Common.Sarcastic,
            Tone.Angry => DA.Lang.Translations.Common.Angry,
            Tone.SocialMedia => DA.Lang.Translations.Common.SocialMedia,
            Tone.Texting => DA.Lang.Translations.Common.Texting,
            _ => throw new ArgumentOutOfRangeException(nameof(tone), tone, null),
        };
    }

    public static string ToPromptString(this Tone tone)
    {
        return tone switch
        {
            Tone.Neutral => "Translate and match the tone of the given sentence.",
            Tone.Custom => "Translate and match the tone of the given sentence.",
            Tone.Casual => "Translate with a casual tone.",
            Tone.Formal => "Translate with a formal tone.",
            Tone.Honorific => "Translate with a respectful tone with honorifics.",
            Tone.Humble => "Translate with a humble tone.",
            Tone.Friendly => "Translate with a friendly tone.",
            Tone.Sarcastic => "Translate with a sarcastic tone.",
            Tone.Angry => "Translate with an angry tone.",
            Tone.SocialMedia => "Translate with a tone as if this would be posted on social media.",
            Tone.Texting => "Translate with a tone as if this would be sent in a text message.",
            Tone.Professional => "Translate with a professional tone.",
            _ => throw new ArgumentOutOfRangeException(nameof(tone), tone, null),
        };
    }
}