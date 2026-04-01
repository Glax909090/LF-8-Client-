using System.Windows.Controls;
using UiDesktopApp_LF8.Helpers;
using UiDesktopApp_LF8.Services;
using UiDesktopApp_LF8.ViewModels;
using UiDesktopApp_LF8.ViewModels.Pages;

namespace UiDesktopApp_LF8.Views.Pages
{
    public partial class ComputerMonitorPage : Page
    {
        public ComputerMonitorViewModel ViewModel { get; }

        public ComputerMonitorPage()
        {
            // This constructor is called by WPF UI when navigating via Type
            InitializeComponent();

            if (Storage.CurrentComputer != null)
            {
                var computer = Storage.CurrentComputer;
                ViewModel = new ComputerMonitorViewModel(computer.Value.Key);
                DataContext = ViewModel;
            }
            else
            {
                // Fallback (should not happen)
                ViewModel = new ComputerMonitorViewModel("Unknown Computer");
                DataContext = ViewModel;
            }
        }
    }
}