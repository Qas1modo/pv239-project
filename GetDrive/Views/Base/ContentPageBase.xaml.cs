using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public abstract partial class ContentPageBase : ContentPage
    {
        private readonly IGlobalExceptionService globalExceptionService;
        protected IViewModel viewModel { get; }

        protected ContentPageBase(
            IViewModel viewModel,
            IGlobalExceptionService globalExceptionService)
        {
            this.globalExceptionService = globalExceptionService;
            BindingContext = this.viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await viewModel.OnAppearingAsync();
            }
            catch (Exception e)
            {
                globalExceptionService.HandleException(e);
            }
        }
    }
}
