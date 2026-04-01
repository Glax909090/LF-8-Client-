using Microsoft.Extensions.Hosting;
using UiDesktopApp_LF8.Views.Pages;
using UiDesktopApp_LF8.Views.Windows;
using Wpf.Ui;

namespace UiDesktopApp_LF8.Services;

public class ApplicationHostService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigationService _navigationService;
    private INavigationWindow _navigationWindow;

    public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService)
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await HandleActivationAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;

    private async Task HandleActivationAsync()
    {
        if (!Application.Current.Windows.OfType<MainWindow>().Any())
        {
            _navigationWindow = (_serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            _navigationWindow!.ShowWindow();

            // Navigiert direkt zum Dashboard statt zur leeren Startseite
            _navigationService.Navigate(typeof(LoginPage));
        }

        await Task.CompletedTask;
    }
}
