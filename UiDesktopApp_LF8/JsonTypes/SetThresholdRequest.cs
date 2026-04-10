namespace UiDesktopApp_LF8.JsonTypes
{
	internal class SetThresholdRequest
	{
		public string? Hostname { get; set; }
		public string? AuthToken { get; set; }
		public float? CpuLimit { get; set; }
		public float? RamLimit { get; set; }
		public float? DiskLimit { get; set; }
	}
}
