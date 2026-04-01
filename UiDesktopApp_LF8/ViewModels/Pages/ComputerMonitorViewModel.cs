using System.Collections.ObjectModel;
using UiDesktopApp_LF8.Models;

namespace UiDesktopApp_LF8.ViewModels.Pages
{
    public partial class ComputerMonitorViewModel : ObservableObject
    {
        [ObservableProperty]
        private string computerName;

        public CpuMetric Cpu { get; } = new();
        public RamMetric Ram { get; } = new();

        public ObservableCollection<DriveMetric> Drives { get; } = new();
        public ObservableCollection<NetworkMetric> Networks { get; } = new();

        public ComputerMonitorViewModel(string computerName)
        {
            ComputerName = computerName;
            // Load real data here (or via a service)
        }

        // Example: Update metrics from real monitoring
        public void UpdateMetrics(/* your data */)
        {
            // Cpu.Usage = ... etc.
        }
    }
}
