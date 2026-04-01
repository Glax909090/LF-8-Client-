using Newtonsoft.Json;

namespace UiDesktopApp_LF8.JsonTypes
{
	internal class MemoryUsage
	{
		public float Used { get; set; } = 0;
		public float Total { get; set; } = 0;
		public string Unit { get; set; } = "";
	}

	internal class DiskIO
	{
		public float CurrentRead { get; set; } = 0;
		public float CurrentWrite { get; set; } = 0;
	}

	internal class DiskUsage
	{
		public string Name { get; set; } = "";
		public float TotalUsed { get; set; } = 0;
		public float Total { get; set; } = 0;
	}

	internal class NetworkInterface
	{
		public string Name { get; set; } = "";
		public float CurrentDownload { get; set; } = 0;
		public float CurrentUpload { get; set; } = 0;
	}

    internal class MonitoringData
    {
        public string Timestamp { get; set; } = "";
        public float Cpu { get; set; } = 0;
        public MemoryUsage Memory { get; set; } = new();
        public DiskIO DiskIO { get; set; } = new();
        public DiskUsage[] Disks { get; set; } = [];
        public NetworkInterface[] NetworkInterfaces { get; set; } = [];
    }
}
