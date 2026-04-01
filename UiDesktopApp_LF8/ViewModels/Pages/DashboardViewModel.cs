namespace UiDesktopApp_LF8.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _counter = 0;

        // Diese Zeile muss HIER drinnen stehen:
        public string CurrentComputerName => Environment.MachineName;

        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }
    }
}   