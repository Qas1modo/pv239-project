using CommunityToolkit.Mvvm.ComponentModel;

namespace GetDrive.ViewModels;

public class ViewModelBase : ObservableObject
{
    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }
}
