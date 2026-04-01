using CommunityToolkit.Mvvm.ComponentModel;

namespace UiDesktopApp_LF8.Models
{
    public partial class CpuMetric : ObservableObject
    {
        [ObservableProperty] private double usage = 34;
        public string UsageText => $"{Usage:F0}%";
    }

    public partial class RamMetric : ObservableObject
    {
        [ObservableProperty] private double usage = 62;
        public string UsageText => $"{Usage:F0}%";
    }

    public partial class DriveMetric : ObservableObject
    {
        [ObservableProperty] private string driveLetter = "C:";
        [ObservableProperty] private double percentUsed = 75;
        [ObservableProperty] private string usedText = "375 GB von 500 GB belegt";

        public string UsageText => $"{PercentUsed:F0}%";
    }

    public partial class NetworkMetric : ObservableObject
    {
        [ObservableProperty] private string name = "Ethernet";
        [ObservableProperty] private string recvSpeed = "1.2 Mbps";
        [ObservableProperty] private string sendSpeed = "300 Kbps";
        [ObservableProperty] private double usage = 45;
    }
}
