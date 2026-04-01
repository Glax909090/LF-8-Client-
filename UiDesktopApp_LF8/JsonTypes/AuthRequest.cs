namespace UiDesktopApp_LF8.JsonTypes
{
	internal class AuthRequest
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
	}

	internal class AuthResponse
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
		public string? AuthToken { get; set; }
	}
}
