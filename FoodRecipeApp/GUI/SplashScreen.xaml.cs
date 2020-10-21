using System;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for SplashScreen.xaml
	/// </summary>
	public partial class SplashScreen : MetroWindow
	{
		private DispatcherTimer _timer;
		public SplashScreen()
		{
			InitializeComponent();

			/*ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
			ThemeManager.Current.SyncTheme();*/
		}

		private void LaunchGitHubSite(object sender, RoutedEventArgs e)
		{
			// Launch the GitHub site...
		}

		private void DeployCupCakes(object sender, RoutedEventArgs e)
		{
			// deploy some CupCakes...
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			_timer = new DispatcherTimer(TimeSpan.FromMilliseconds(200),
				DispatcherPriority.Normal,
				(o, args) =>
				{
					/*TheProgressBar.Value = DateTime.Now.Millisecond;
					TheOtherProgressBar.Value = DateTime.Now.Millisecond;*/
				},
				Dispatcher);
			_timer.Start();
		}

		private void MainWindow_OnUnloaded(object sender, RoutedEventArgs e)
		{
			_timer?.Stop();
		}

		private MetroWindow _accentThemeTestWindow;

		private void ChangeAppStyleButtonClick(object sender, RoutedEventArgs e)
		{
			if (_accentThemeTestWindow != null)
			{
				_accentThemeTestWindow.Activate();
				return;
			}

			_accentThemeTestWindow = new AccentStyleWindow
			{
				Owner = this
			};
			_accentThemeTestWindow.Closed += (o, args) => _accentThemeTestWindow = null;
			_accentThemeTestWindow.Left = this.Left + this.ActualWidth / 2.0;
			_accentThemeTestWindow.Top = this.Top + this.ActualHeight / 2.0;
			_accentThemeTestWindow.Show();
		}
	}
}
