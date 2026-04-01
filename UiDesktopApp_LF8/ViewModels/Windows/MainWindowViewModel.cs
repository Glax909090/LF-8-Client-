using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using RestSharp;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UiDesktopApp_LF8.Helpers;
using UiDesktopApp_LF8.JsonTypes;
using UiDesktopApp_LF8.Models;           // Add this
using UiDesktopApp_LF8.Views.Pages;
using Wpf.Ui.Controls;

namespace UiDesktopApp_LF8.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string applicationTitle = "Hardware Monitor - LF8";

        // Static menu items (Dashboard, etc.)
        [ObservableProperty]
        private ObservableCollection<object> menuItems = new();

        // Dynamic computers will be added here
        [ObservableProperty]
        private ObservableCollection<object> computerMenuItems = new();

        [ObservableProperty]
        private ObservableCollection<object> footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Alert-Einstellungen",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Alert24 },
                TargetPageType = typeof(SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> trayMenuItems = new()
        {
            new MenuItem { Header = "Monitor öffnen", Tag = "tray_home" }
        };

        public MainWindowViewModel()
        {
            // Add static items
            MenuItems.Add(new NavigationViewItem
            {
                Content = "Dashboard",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(ComputerMonitorPage)
            });

            // Computers will be loaded later via LoadComputersAsync()
        }

        /// <summary>
        /// Call this after successful login
        /// </summary>
        public void LoadComputers()
        {
            RestClient client = new(Storage.Url);
            RestRequest request = new("/get-data", Method.Post);
            request.AddBody(new GetDataRequest
            {
                Client = null,
                AuthToken = Storage.AuthToken
            });
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return;
            }

            var responseComputers = JsonConvert.DeserializeObject<Dictionary<string, MonitoringData>>(response.Content!)!;

            foreach (var computer in responseComputers)
            {
                var navItem = new NavigationViewItem
                {
                    Content = computer.Key,
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Desktop24 },
                    TargetPageType = typeof(ComputerMonitorPage),
                    Tag = computer
                };

                ComputerMenuItems.Add(navItem);
            }
        }
    }
}
