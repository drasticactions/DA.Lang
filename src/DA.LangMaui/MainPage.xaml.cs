using System.ComponentModel;
using DA.Lang.ViewModels;
using DA.UI.Tools;

namespace DA.LangMaui;

public partial class MainPage : ContentPage
{
	private readonly TranslationViewModel _vm;

	public MainPage(TranslationViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = _vm = vm;
#if MACCATALYST || IOS
		this.TopEditor.FontFamily = "Helvetica Neue";
		this.TopEditor.FontSize = 18;
		this.TopEditor.FontAttributes = FontAttributes.Bold;

		this.BottomEditor.FontFamily = "Helvetica Neue";
		this.BottomEditor.FontSize = 18;
		this.BottomEditor.FontAttributes = FontAttributes.Bold;
#endif
	}

	/// <inheritdoc/>
	protected override void OnHandlerChanged()
	{
		base.OnHandlerChanged();
#if MACCATALYST
		this.SettingsRow.IsVisible = false;
#elif IOS
        this.SettingsButton.Text = string.Empty;
        var gearIcon = UIKit.UIImage.GetSystemImage("gearshape");
        var button = (UIKit.UIButton)this.SettingsButton.Handler!.PlatformView;
        button!.SetImage(gearIcon, UIKit.UIControlState.Normal);
#elif WINDOWS
        this.SettingsButton.Text = string.Empty;
        var gearIcon = Microsoft.UI.Xaml.Controls.Symbol.Setting;
        var button = (Microsoft.UI.Xaml.Controls.Button)this.SettingsButton.Handler!.PlatformView;
        button!.Content = new Microsoft.UI.Xaml.Controls.SymbolIcon() { Symbol = gearIcon };
#endif
	}

	private void SettingsButton_OnClicked(object? sender, EventArgs e)
	{
		lock (this)
		{
			var settings = this.Handler?.MauiContext?.Services?.GetRequiredService<ModalNavigationSettingsPage>();
			this.Navigation.PushModalAsync(settings).FireAndForgetSafeAsync();
		}
	}

	private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
	{
	}

	private void ToneGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
	{
		var tonePage = this.Handler?.MauiContext?.Services?.GetRequiredService<ToneSelectionPage>();
		this.Navigation.PushModalAsync(tonePage).FireAndForgetSafeAsync();
	}
}

