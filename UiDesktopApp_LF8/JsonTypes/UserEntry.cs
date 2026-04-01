namespace UiDesktopApp_LF8.JsonTypes
{
	internal class UserEntry
	{
		public string Username { get; set; }
		public string HashedPassword { get; set; }
		public string? AuthToken { get; set; }
	}
}
