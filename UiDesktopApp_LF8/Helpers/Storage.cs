using UiDesktopApp_LF8.JsonTypes;

namespace UiDesktopApp_LF8.Helpers
{
    internal class Storage
    {
        public static string AuthToken { get; set; } = "";
        public static string Url { get; set; } = "http://localhost:8000";
        public static KeyValuePair<string, MonitoringData>? CurrentComputer { get; set; }
    }
}
