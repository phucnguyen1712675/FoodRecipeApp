using System;
using System.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using FoodRecipeApp.DTO;
using MahApps.Metro.Controls;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for SplashScreen.xaml
	/// </summary>
	public partial class SplashScreen : MetroWindow
	{
		public SplashScreen()
		{
			InitializeComponent();
		}

		public double Progress
		{
			get => ProgressBar.Value;
			set => ProgressBar.Value = value;
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

		private void DoNotShowPlashScreenCheckBox_OnChecked(object sender, RoutedEventArgs e)
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
			config.Save(ConfigurationSaveMode.Minimal);
		}
	}
}
