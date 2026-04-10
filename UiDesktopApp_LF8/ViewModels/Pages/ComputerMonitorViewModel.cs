using System.Collections.ObjectModel;
using UiDesktopApp_LF8.JsonTypes;
using UiDesktopApp_LF8.Models;
using RestSharp;
using UiDesktopApp_LF8.Helpers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UiDesktopApp_LF8.ViewModels.Pages
{
    public partial class ComputerMonitorViewModel : ObservableObject
    {
        [ObservableProperty]
        private string computerName;

        private RestClient client;

        public CpuMetric Cpu { get; } = new();
        public RamMetric Ram { get; } = new();

        public ObservableCollection<DriveMetric> Drives { get; } = new();
        public ObservableCollection<NetworkMetric> Networks { get; } = new();

        public ComputerMonitorViewModel(string computerName)
        {
            ComputerName = computerName;
            client = new("http://localhost:8000");
            UpdateMetricsAsync();
        }

        public async Task UpdateMetricsAsync()
        {
            if (string.IsNullOrEmpty(ComputerName))
                return;

            //Trace.WriteLine($"Updating metrics for {ComputerName}");
            RestRequest request = new("/get-data", Method.Post);
            request.AddBody(new GetDataRequest
            {
                Client = ComputerName,
                AuthToken = Storage.AuthToken
            });
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return;
            }

            Dictionary<string, MonitoringData> responseComputers = JsonConvert.DeserializeObject<Dictionary<string, MonitoringData>>(response.Content!)!;

            if (!responseComputers.ContainsKey(ComputerName))
            {
                return;
            }

            MonitoringData computerData = responseComputers[ComputerName];

            Cpu._usage = computerData.Cpu;
            Ram.totalRam = computerData.Memory.Total;
            Ram.ramUsed = computerData.Memory.Used;
            Ram._usage = (computerData.Memory.Used / computerData.Memory.Total) * 100;

            Drives.Clear();
            Networks.Clear();

            foreach(var disk in computerData.Disks)
            {
                Drives.Add(new DriveMetric
                {
                    DriveLetter = disk.Name,
                    totalDisk = disk.Total,
                    usedDisk = disk.TotalUsed,
                    _percentUsed = (disk.TotalUsed / disk.Total) * 100
                });
            }

            foreach(var networkInterface in computerData.NetworkInterfaces)
            {
                Networks.Add(new NetworkMetric
                {
                    Name = networkInterface.Name,
                    RecvSpeed = $"{Math.Round(networkInterface.CurrentDownload, 2)}Mbps",
                    SendSpeed = $"{Math.Round(networkInterface.CurrentUpload, 2)}Mbps"
                });
            }
        }
    }
}
