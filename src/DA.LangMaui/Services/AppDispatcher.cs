using DA.UI.Services;

namespace DA.LangMaui.Services;

public class AppDispatcher : IAppDispatcher
{
    public bool Dispatch(Action action)
    {
        return Microsoft.Maui.Controls.Application.Current!.Dispatcher.Dispatch(action);
    }
}