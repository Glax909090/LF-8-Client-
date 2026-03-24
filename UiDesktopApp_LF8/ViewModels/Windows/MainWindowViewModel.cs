using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace UiDesktopApp_LF8.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Hardware Monitor - LF8";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                // Hier steht jetzt dein Computername statt "System Monitor"
                Content = "LF8-ADMIN-PC",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Info24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Alert-Einstellungen",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Alert24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        }; // <--- HIER hat das Semikolon gefehlt!

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Monitor öffnen", Tag = "tray_home" }
        };
    } // Diese schließt die Klasse (MainWindowViewModel)
}     // Diese schließt den Namespace (UiDesktopApp_LF8.ViewModels.Windows)