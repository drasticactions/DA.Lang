using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA.Lang.ViewModels;
using DA.UI.Tools;

namespace DA.LangMaui;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel _vm;
    
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = _vm = vm;
        
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        this.Navigation.PopModalAsync().FireAndForgetSafeAsync();
    }
}