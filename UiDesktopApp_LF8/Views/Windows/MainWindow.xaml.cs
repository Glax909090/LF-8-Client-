using UiDesktopApp_LF8.ViewModels.Windows;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace UiDesktopApp_LF8.Views.Windows
{
    public partial class MainWindow : INavigationWindow
    {
        public MainWindowViewModel ViewModel { get; }
        private readonly INavigationService _navigationService;

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationViewPageProvider navigationViewPageProvider,
            INavigationService navigationService
        )
        {
            ViewModel = viewModel;
            _navigationService = navigationService; // Service lokal speichern
            DataContext = this;

            SystemThemeWatcher.Watch(this);

            InitializeComponent();
            SetPageService(navigationViewPageProvider);

            _navigationService.SetNavigationControl(RootNavigation);

            // Dieses Event sorgt dafür, dass beim Start direkt das Dashboard geladen wird
            Loaded += (s, e) => _navigationService.Navigate(typeof(Views.Pages.DashboardPage));
        }

        #region INavigationWindow methods

        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void SetPageService(INavigationViewPageProvider navigationViewPageProvider) => RootNavigation.SetPageProviderService(navigationViewPageProvider);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        #endregion INavigationWindow methods

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        // Diese Methoden müssen für das Interface vorhanden sein, 
        // greifen aber auf die obigen Implementierungen zu
        INavigationView INavigationWindow.GetNavigation() => GetNavigation();

        public void SetServiceProvider(IServiceProvider serviceProvider) { }
    }
}