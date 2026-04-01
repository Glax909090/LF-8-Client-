using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UiDesktopApp_LF8.Helpers;
using UiDesktopApp_LF8.ViewModels.Pages;

namespace UiDesktopApp_LF8.Views.Pages
{
    public partial class ComputerMonitorPage : Page
    {
        private readonly DispatcherTimer _refreshTimer;

        public ComputerMonitorViewModel ViewModel { get; private set; } = null!;

        public static ComputerMonitorPage? CurrentInstance { get; private set; }

        public ComputerMonitorPage()
        {
            InitializeComponent();

            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _refreshTimer.Tick += async (s, e) =>
            {
                if (ViewModel != null)
                    await ViewModel.UpdateMetricsAsync();
            };

            // Hook into page lifecycle
            Loaded += ComputerMonitorPage_Loaded;
            Unloaded += ComputerMonitorPage_Unloaded;
        }

        public void RefreshPageContent()
        {
            if (Storage.CurrentComputer != null)
            {
                var computer = Storage.CurrentComputer;
                ViewModel = new ComputerMonitorViewModel(computer.Value.Key);   // adjust if you use .Name
                DataContext = ViewModel;

                _refreshTimer.Start();
                Trace.WriteLine($"Started refreshing metrics for {computer.Value.Key}");
            }
            else
            {
                ViewModel = new ComputerMonitorViewModel("Unknown Computer");
                DataContext = ViewModel;
            }
        }

        private void ComputerMonitorPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Remove handler so it doesn't run multiple times
            //Loaded -= ComputerMonitorPage_Loaded;
            CurrentInstance = this;
            RefreshPageContent();
        }

        private void ComputerMonitorPage_Unloaded(object sender, RoutedEventArgs e)
        {
            _refreshTimer.Stop();
            Trace.WriteLine("Stopped refreshing metrics (page unloaded)");
        }

        // Optional: Also stop timer when user switches pages via navigation
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            // Extra safety - stop timer if this page is removed from visual tree
            if (visualRemoved == this && _refreshTimer.IsEnabled)
            {
                _refreshTimer.Stop();
            }
        }
    }
}
