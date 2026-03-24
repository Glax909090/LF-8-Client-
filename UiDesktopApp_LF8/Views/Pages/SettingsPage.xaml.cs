using UiDesktopApp_LF8.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp_LF8.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
