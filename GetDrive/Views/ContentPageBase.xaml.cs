using GetDrive.ViewModels;

namespace GetDrive.Views;

public partial class ContentPageBase
{
    private readonly ViewModelBase viewModel;

    public ContentPageBase(ViewModelBase viewModel)
    {
        InitializeComponent();

        this.viewModel = viewModel;
        BindingContext = this.viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.OnAppearingAsync();
    }
}