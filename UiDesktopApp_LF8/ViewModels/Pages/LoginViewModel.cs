using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using UiDesktopApp_LF8.Helpers;
using UiDesktopApp_LF8.JsonTypes;
using UiDesktopApp_LF8.ViewModels.Windows;
using UiDesktopApp_LF8.Views.Pages;
using UiDesktopApp_LF8.Views.Windows;
using Wpf.Ui;

namespace UiDesktopApp_LF8.ViewModels.Pages
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly RestClient client;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            client = new(Storage.Url);
            IsBusy = false;
        }

        // Properties bound from XAML
        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool isBusy = false;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        // The Login Command (bound to the button)
        [RelayCommand]
        private async Task LoginAsync()
        {
            Console.WriteLine("login start");
            Trace.WriteLine("login start");
            // Clear previous error
            ErrorMessage = string.Empty;
            IsBusy = true;

            try
            {
                // Simulate network delay / real API call
                await Task.Delay(1500);

                // TODO: Replace this with your real authentication logic
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Please enter both username and password.";
                    return;
                }

                RestRequest request = new("/auth", Method.Post);
                request.AddBody(new AuthRequest
                {
                    Username = Username,
                    Password = Password
                });
                var response = client.Execute(request);

                AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Content!)!;

                if (!authResponse.Success)
                {
                    ErrorMessage = authResponse.Message!;
                } else
                {
                    Storage.AuthToken = authResponse.AuthToken!;
                    if (Application.Current.MainWindow is MainWindow window)
                    {
                        if (window.ViewModel is MainWindowViewModel mainVM)
                        {
                            Trace.WriteLine("loading computers");
                            mainVM.LoadComputers();
                        }
                    }
                    //_navigationService.Navigate(typeof(ComputerMonitorPage));
                    
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Optional: Clear error when user starts typing again
        partial void OnUsernameChanged(string value)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = string.Empty;
        }

        partial void OnPasswordChanged(string value)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = string.Empty;
        }
    }
}