// <copyright file="ModalNavigationSettingsPage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;


namespace DA.LangMaui;

/// <summary>
/// Modal Navigation Settings Page.
/// </summary>
public class ModalNavigationSettingsPage : Microsoft.Maui.Controls.NavigationPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModalNavigationSettingsPage"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public ModalNavigationSettingsPage(IServiceProvider provider)
        : base(provider.GetRequiredService<SettingsPage>())
    {
        // Show a form sheet for modals on iOS.
        this.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}