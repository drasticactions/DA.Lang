using DA.Lang.Models;
using DA.Lang.Services;
using DA.UI.Services;
using DA.UI.ViewModels;

namespace DA.Lang.ViewModels;

public class ToneSelectionViewModel : BaseViewModel
{
    private OpenAITranslationService _service;
    private ToneItem _selectedTone;
    
    public ToneSelectionViewModel(OpenAITranslationService service, IAppDispatcher dispatcher, IErrorHandler errorHandler, IAsyncCommandFactory asyncCommandFactory)
        : base(dispatcher, errorHandler, asyncCommandFactory)
    {
        _service = service;
        this.Tones = ToneItem.GetTones().ToList();
        this.SelectedTone = this.Tones.First(tone => tone.Tone == _service.Tone);
    }
    
    public List<ToneItem> Tones { get; }
    
    public ToneItem SelectedTone
    {
        get => _selectedTone;
        set
        {
            if (value is null)
            {
                return;
            }
            
            if (_selectedTone == value)
            {
                return;
            }
            
            _selectedTone = value;
            _service.Tone = value.Tone;
            OnPropertyChanged();
        }
    }
}