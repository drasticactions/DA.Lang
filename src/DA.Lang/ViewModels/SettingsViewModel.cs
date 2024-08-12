using DA.Lang.Database;
using DA.Lang.Models;
using DA.Lang.Services;
using DA.UI.Services;
using DA.UI.Tools;
using DA.UI.ViewModels;

namespace DA.Lang.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private OpenAITranslationService _service;
    private LangDatabase _database;
    private Settings _settings;
    
    public SettingsViewModel(LangDatabase database, OpenAITranslationService service, IAppDispatcher dispatcher, IErrorHandler errorHandler, IAsyncCommandFactory asyncCommandFactory)
        : base(dispatcher, errorHandler, asyncCommandFactory)
    {
        _database = database;
        _settings = _database.GetSettings();
        _service = service;
    }
    
    public string OpenAiKey
    {
        get => _settings.OpenAiKey;
        set
        {
            if (_settings.OpenAiKey == value)
            {
                return;
            }
            
            _settings.OpenAiKey = value;
            _service.ApiKey = value;
            _database.UpsertSettingsAsync(_settings).FireAndForgetSafeAsync(this.ErrorHandler);
            OnPropertyChanged();
        }
    }
}