using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA.Lang.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using DA.Lang.ViewModels;
using DA.UI.Tools;

namespace DA.LangMaui;

public partial class ToneSelectionPage : ContentPage
{
    private readonly ToneSelectionViewModel _vm;
    
    public ToneSelectionPage(ToneSelectionViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = _vm = vm;
        
        // Show a form sheet for modals on iOS.
        this.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
    
    private void Button_OnClicked(object? sender, EventArgs e)
    {
        this.Navigation.PopModalAsync().FireAndForgetSafeAsync();
    }
    
    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ToneItem lang)
        {
            this._vm.SelectedTone = lang;
            await this.Navigation.PopModalAsync();
            this.ListView.SelectedItem = null;
        }
    }
}