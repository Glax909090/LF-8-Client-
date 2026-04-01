using Newtonsoft.Json;
using RestSharp;
using System.Collections;
using System.Windows.Controls;
using UiDesktopApp_LF8.Helpers;
using UiDesktopApp_LF8.JsonTypes;
using UiDesktopApp_LF8.ViewModels.Pages;
using UiDesktopApp_LF8.ViewModels.Windows;
using UiDesktopApp_LF8.Views.Pages;
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

            RootNavigation.ItemInvoked += RootNavigation_ItemInvoked;

            _navigationService.SetNavigationControl(RootNavigation);

            Loaded += async (s, e) => ViewModel.LoadComputers();
        }

        private void RootNavigation_ItemInvoked(NavigationView sender, RoutedEventArgs e)
        {
            if (sender.SelectedItem is NavigationViewItem item)
            {
                // Is this a computer item?
                if (item.Tag is KeyValuePair<string, MonitoringData> computer)
                {
                    // Remember which computer we want to show
                    Storage.CurrentComputer = computer;

                    // Navigate using the Type (this is what WPF UI expects)
                    sender.Navigate(typeof(ComputerMonitorPage));
                    return;
                }

                // Handle normal static items (Dashboard, Settings, etc.)
                if (item.TargetPageType != null)
                {
                    sender.Navigate(item.TargetPageType);
                }
            }
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