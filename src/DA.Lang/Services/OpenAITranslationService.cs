using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using DA.Lang.Models;
using DA.Lang.Services;
using DA.Lang.Exceptions;

namespace DA.Lang.Services;

public class OpenAITranslationService : ITranslationService, INotifyPropertyChanged
{
    private Tone _tone;
    private string _customTone;
    private string _apiKey;
    private CultureInfo _culture = CultureInfo.InvariantCulture;
    
    public Tone Tone
    {
        get => _tone;
        set => SetField(ref _tone, value);
    }

    public CultureInfo Culture
    {
        get => _culture;
        set => SetField(ref _culture, value);
    }

    public string CustomTone
    {
        get => _customTone;
        set => SetField(ref _customTone, value);
    }
    
    public string ApiKey
    {
        get => _apiKey;
        set => SetField(ref _apiKey, value);
    }
    
    public string TranslationService => "OpenAI";
    public event EventHandler? RaiseOnExecuteChanged;

    public bool CanExecute => !string.IsNullOrWhiteSpace(ApiKey) && !this.Culture.IsNeutralCulture;

    public async Task<Translation?> TranslateAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            throw new APIKeyMissingException();
        }
        
        if (string.IsNullOrEmpty(text))
            return null;

        var openAiClient = new global::OpenAI.OpenAIClient(ApiKey);
        var systemMessage = new global::OpenAI.Chat.SystemChatMessage(this.GenerateSystemMessage());
        var userMessage = new global::OpenAI.Chat.UserChatMessage(text);
        var chat = openAiClient.GetChatClient("gpt-4o");
        var response = await chat.CompleteChatAsync(systemMessage, userMessage);
        if (response is null)
        {
            throw new OpenAIGenerationException();
        }
        
        if (response.Value.Content is null)
        {
            throw new OpenAIGenerationException();
        }
        
        var responseText = response.Value.Content.FirstOrDefault()?.Text ?? throw new OpenAIGenerationException();
        return new Translation()
        {
            OriginalText = text,
            TranslatedText = responseText,
            Tone = Tone,
            Language = Culture.IetfLanguageTag,
            TranslationService = TranslationService,
            TranslatedAt = DateTime.UtcNow
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        this.RaiseOnExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }
}