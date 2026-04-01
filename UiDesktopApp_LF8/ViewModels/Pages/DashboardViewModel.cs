using System.Collections.ObjectModel;
using UiDesktopApp_LF8.Models;

namespace UiDesktopApp_LF8.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private string computerName = "LF8-ADMIN-WORKSTATION";

        // Fixed metrics (CPU + RAM)
        public CpuMetric Cpu { get; } = new();
        public RamMetric Ram { get; } = new();

        // Dynamic collections
        public ObservableCollection<DriveMetric> Drives { get; } = new();
        public ObservableCollection<NetworkMetric> Networks { get; } = new();

        public DashboardViewModel()
        {
            // ← You can load real data here (SystemInformation, WMI, etc.)
            Drives.Add(new DriveMetric { DriveLetter = "C:", PercentUsed = 75, UsedText = "375 GB von 500 GB belegt" });
            Drives.Add(new DriveMetric { DriveLetter = "D:", PercentUsed = 42, UsedText = "210 GB von 500 GB belegt" });

            Networks.Add(new NetworkMetric { Name = "Ethernet (LAN)", RecvSpeed = "1.2 Mbps", SendSpeed = "300 Kbps", Usage = 45 });
            Networks.Add(new NetworkMetric { Name = "Wi-Fi", RecvSpeed = "150 Kbps", SendSpeed = "45 Kbps", Usage = 12 });
        }

        // Call this whenever you want to update values live
        public void RefreshMetrics()
        {
            Cpu.Usage = 28;   // example
            Ram.Usage = 67;

            // You can also update individual drives/networks:
            if (Drives.Count > 0) Drives[0].PercentUsed = 81;
        }
    }
}
