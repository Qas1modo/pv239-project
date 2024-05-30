using GetDrive.Services;
using GetDrive.ViewModels;

namespace GetDrive.Views
{
    public partial class ReviewView : ContentPageBase
    {
        public ReviewView(ReviewViewModel viewModel, IGlobalExceptionService globalExceptionService)
            : base(viewModel, globalExceptionService)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}
