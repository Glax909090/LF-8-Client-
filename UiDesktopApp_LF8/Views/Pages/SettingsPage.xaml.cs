using System.Diagnostics;
using UiDesktopApp_LF8.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp_LF8.Views.Pages
{
	public partial class SettingsPage : INavigableView<SettingsViewModel>
	{
		public SettingsViewModel ViewModel { get; }

		public SettingsPage(SettingsViewModel viewModel)
		{
			ViewModel = viewModel;
			DataContext = viewModel;

			InitializeComponent();

			Loaded += async (object sender, RoutedEventArgs args) =>
			{
				ViewModel.Clients.Clear();
				Trace.WriteLine("refreshing clients");
				await ViewModel.LoadClients();
			};
		}
	}
}
