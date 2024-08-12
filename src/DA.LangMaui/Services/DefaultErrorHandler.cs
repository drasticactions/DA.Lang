using DA.UI.Services;

namespace DA.LangMaui.Services;

public class DefaultErrorHandler : IErrorHandler
{
    public void HandleError(Exception ex)
    {
        lock (this)
        {
            if (Microsoft.Maui.Controls.Application.Current is not null)
            {
                Microsoft.Maui.Controls.Application.Current.MainPage?.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}