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
		[JsonProperty("readMBps")]
		public float CurrentRead { get; set; } = 0;
		[JsonProperty("writeMBps")]
		public float CurrentWrite { get; set; } = 0;
	}

	internal class DiskUsage
	{
		public string Name { get; set; } = "";
		[JsonProperty("usedGiB")]
		public float TotalUsed { get; set; } = 0;
		[JsonProperty("totalGiB")]
		public float Total { get; set; } = 0;
	}

	internal class NetworkInterface
	{
		[JsonProperty("interface")]
		public string Name { get; set; } = "";
		[JsonProperty("downloadMbit")]
		public float CurrentDownload { get; set; } = 0;
		[JsonProperty("uploadMbit")]
		public float CurrentUpload { get; set; } = 0;
	}

	internal class MonitoringData
	{
		public string Timestamp { get; set; } = "";
		[JsonProperty("ram")]
		public MemoryUsage Memory { get; set; } = new();
		public DiskIO DiskIO { get; set; } = new();
		public DiskUsage[] Disks { get; set; } = [];
		[JsonProperty("network")]
		public NetworkInterface[] NetworkInterfaces { get; set; } = [];
	}
}
