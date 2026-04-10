using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Appearance;

namespace UiDesktopApp_LF8.ViewModels.Pages
{
	using Newtonsoft.Json;
	using RestSharp;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using UiDesktopApp_LF8.Helpers;
	using UiDesktopApp_LF8.JsonTypes;
	using UiDesktopApp_LF8.Models;

	public partial class SettingsViewModel
	{
		public ObservableCollection<ClientAlertConfig> Clients { get; set; }

		public SettingsViewModel()
		{
			Clients = [];
		}

		//This is called from SettingsPage.xaml.cs
		public async Task LoadClients()
		{
			if (Storage.AuthToken == null)
			{
				return;
			}

			RestClient client = new(Storage.Url);
			RestRequest request = new("/get-threshold", Method.Post);
			request.AddJsonBody(new GetThresholdRequest
			{
				Hostname = null,
				AuthToken = Storage.AuthToken
			});
			var response = await client.ExecuteAsync(request);

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return;
			}

			Dictionary<string, ThresholdData> responseData = JsonConvert.DeserializeObject<Dictionary<string, ThresholdData>>(response.Content!)!;

			foreach(var _client in responseData)
			{
				Clients.Add(new ClientAlertConfig
				{
					Name = _client.Key,
					CpuWarning = _client.Value.CpuLimit,
					RamWarning = _client.Value.RamLimit,
					DiskWarning = _client.Value.DiskLimit
				});
			}
		}

		[RelayCommand]
		private void SaveClient(ClientAlertConfig client)
		{
			var name = client.Name;
			var cpu = client.CpuWarning;
			var ram = client.RamWarning;
			var disk = client.DiskWarning;

			RestClient restClient = new(Storage.Url);
			RestRequest request = new("/set-threshold", Method.Post);
			request.AddJsonBody(new SetThresholdRequest
			{
				Hostname = name,
				CpuLimit = cpu,
				RamLimit = ram,
				DiskLimit = disk,
				AuthToken = Storage.AuthToken
			});

			var response = restClient.Execute(request);

			Trace.WriteLine(response.Content);
		}
	}
}
