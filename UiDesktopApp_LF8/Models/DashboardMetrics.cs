using CommunityToolkit.Mvvm.ComponentModel;

namespace UiDesktopApp_LF8.Models
{
    public partial class CpuMetric : ObservableObject
    {
        public double _usage
        {
            set
            {
                PercentUsed = value;
                UsageText = $"{PercentUsed:F0}%";
            }
        }
        [ObservableProperty] private double percentUsed = 34;
        [ObservableProperty] private string usageText = "";
    }

    public partial class RamMetric : ObservableObject
    {
        public double totalRam;
        public double ramUsed;
        public double _usage
        {
            set
            {
                PercentUsed = value;
                UsageText = $"{PercentUsed:F0}%";
                UsedText = $"{Math.Round(ramUsed, 2)}GiB / {Math.Round(totalRam, 2)}GiB Used";
            }
        }
        [ObservableProperty] private double percentUsed = 34;
        [ObservableProperty] private string usageText = "";
        [ObservableProperty] private string usedText = "";
    }

    public partial class DriveMetric : ObservableObject
    {
        [ObservableProperty] private string driveLetter = "C:";
        public double totalDisk = 0;
        public double usedDisk = 0;
        public double _percentUsed { set
            {
                PercentUsed = value;
                UsageText = $"{PercentUsed:F0}%";
                UsedText = $"{Math.Round(usedDisk, 2)}GiB / {Math.Round(totalDisk, 2)}GiB Used";
            }
        }
        [ObservableProperty] private double percentUsed = 75;
        [ObservableProperty] private string usageText = "";
        [ObservableProperty] private string usedText = "";
    }

    public partial class NetworkMetric : ObservableObject
    {
        [ObservableProperty] private string name = "Ethernet";
        [ObservableProperty] private string recvSpeed = "1.2 Mbps";
        [ObservableProperty] private string sendSpeed = "300 Kbps";
    }
}
