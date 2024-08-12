using System.Globalization;
using DA.Lang.Database;
using DA.Lang.Services;
using DA.Lang.ViewModels;
using DA.LangMaui.Services;
using DA.UI.Services;
using Microsoft.Extensions.Logging;

namespace DA.LangMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
#if MACCATALYST
		// Changes buttons to match iPad behavior.
		// This allows us to keep colors and styles.
		Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("ButtonChange", (handler, view) =>
		{
			handler.PlatformView.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
			handler.PlatformView.Layer.CornerRadius = 5;
			handler.PlatformView.ClipsToBounds = true;
		});

		// Adds toolbar to window.
		Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping("WindowChange", (handler, view) =>
		{
			if (handler.PlatformView?.WindowScene?.Titlebar != null && view is Window win)
			{
				var toolbar = new AppKit.NSToolbar();
				toolbar.Delegate = new ToolbarDelegate(win);
				toolbar.DisplayMode = AppKit.NSToolbarDisplayMode.Icon;

				handler.PlatformView.WindowScene.Titlebar.Toolbar = toolbar;
				handler.PlatformView.WindowScene.Titlebar.ToolbarStyle = UIKit.UITitlebarToolbarStyle.Automatic;
				handler.PlatformView.WindowScene.Titlebar.Toolbar.Visible = true;
			}
		});
#endif
		var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		var connectionString = $"Filename={Path.Combine(path, "lang.db")}";
		var database = new LangDatabase(connectionString);
		var settings = database.GetSettings();
		var openAiService = new OpenAITranslationService()
		{
			ApiKey = settings.OpenAiKey,
			Culture = CultureInfo.GetCultureInfoByIetfLanguageTag(settings.DefaultOutputLanguage)
		};
		var builder = MauiApp.CreateBuilder();

		builder.Services.AddSingleton<IAppDispatcher, AppDispatcher>();
		builder.Services.AddSingleton<IErrorHandler, DefaultErrorHandler>();
		builder.Services.AddSingleton<IAsyncCommandFactory, AsyncCommandFactory>();
		builder.Services.AddSingleton(database);
		builder.Services.AddSingleton(openAiService);
		builder.Services.AddSingleton<TranslationViewModel>();
		builder.Services.AddSingleton<SettingsViewModel>();
		builder.Services.AddSingleton<ToneSelectionViewModel>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<ModalNavigationSettingsPage>();
		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<ToneSelectionPage>();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
