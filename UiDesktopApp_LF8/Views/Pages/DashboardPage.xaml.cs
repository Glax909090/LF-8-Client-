using UiDesktopApp_LF8.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp_LF8.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
