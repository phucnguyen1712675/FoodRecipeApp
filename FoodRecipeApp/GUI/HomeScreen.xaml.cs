using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for HomeScreen.xaml
	/// </summary>
	public partial class HomeScreen : MetroWindow
	{
		public HomeScreen()
		{
			InitializeComponent();
		}

		private void LaunchGitHubSite(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/phucnguyen1712675/FoodRecipeApp");
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
