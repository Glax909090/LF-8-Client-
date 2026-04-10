using Newtonsoft.Json;
using RestSharp;
using System.Collections;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
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
            _navigationService = navigationService;
            DataContext = this;

            SystemThemeWatcher.Watch(this);

            InitializeComponent();
            SetPageService(navigationViewPageProvider);

            _navigationService.SetNavigationControl(RootNavigation);

            Loaded += async (s, e) => ViewModel.LoadComputersAsync();
        }

        private void RootNavigation_SelectionChanged(NavigationView sender, RoutedEventArgs e)
        {
            if (sender.SelectedItem is NavigationViewItem clickedItem)
            {
                if (clickedItem.Tag is KeyValuePair<string, MonitoringData> computer)
                {
                    Trace.WriteLine("computer item");
                    Storage.CurrentComputer = computer;
                    ComputerMonitorPage.CurrentInstance?.RefreshPageContent();
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