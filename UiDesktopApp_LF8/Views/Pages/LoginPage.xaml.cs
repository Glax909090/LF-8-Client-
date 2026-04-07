using UiDesktopApp_LF8.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Controls;

namespace UiDesktopApp_LF8.Views.Pages
{
    public partial class LoginPage : INavigableView<LoginViewModel>
    {
        public LoginViewModel ViewModel { get; }

        public LoginPage(LoginViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb) ViewModel.Password = pb.Password;   // direct access
        }
    }
}
