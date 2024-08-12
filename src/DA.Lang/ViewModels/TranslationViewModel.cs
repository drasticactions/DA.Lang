using DA.Lang.Database;
using DA.Lang.Models;
using DA.Lang.Services;
using DA.Lang.Translations;
using DA.UI.Commands;
using DA.UI.Services;
using DA.UI.ViewModels;

namespace DA.Lang.ViewModels;

public class TranslationViewModel : BaseViewModel
{
    private Translation? _result;
    private LangDatabase _database;
    private OpenAITranslationService _service;
    private string _inputText = string.Empty;
    private string _outputText = string.Empty;
    private IAsyncCommand translateCommand;
    public TranslationViewModel(LangDatabase database, OpenAITranslationService service, IAppDispatcher dispatcher, IErrorHandler errorHandler, IAsyncCommandFactory asyncCommandFactory)
        : base(dispatcher, errorHandler, asyncCommandFactory)
    {
        _database = database;
        _service = service;
        this._service.RaiseOnExecuteChanged += (object? sender, EventArgs e) => this.RaiseCanExecuteChanged();
        this.translateCommand = this.CommandFactory.Create(Translations.Common.TranslateButton, this.TranslateAsync, () => !string.IsNullOrWhiteSpace(this.InputText) && !this.IsBusy && this._service.CanExecute);
    }
    
    public IAsyncCommand TranslateCommand => this.translateCommand;
    
    private Task TranslateAsync(CancellationToken x1, IProgress<int> y2, IProgress<string> z3)
    {
        return this.PerformBusyAsyncTask(async (x, y, z) =>
        {
            Result = await this._service.TranslateAsync(this.InputText);
        }, Common.TranslatingBusyText);
    }
    
    public string TargetLanguage => this._service.Culture.DisplayName;

    public string SelectedTone => this._service.Tone.ToFriendlyString();

    public Translation? Result
    {
        get => _result;
        set => this.SetProperty(ref _result, value, true);
    }
    
    public string InputText
    {
        get => _inputText;
        set => this.SetProperty(ref _inputText, value, true);
    }

    public override void RaiseCanExecuteChanged()
    {
        this.TranslateCommand.RaiseCanExecuteChanged();
        this.OnPropertyChanged(nameof(this.SelectedTone));
        this.OnPropertyChanged(nameof(this.TargetLanguage));
    }
}