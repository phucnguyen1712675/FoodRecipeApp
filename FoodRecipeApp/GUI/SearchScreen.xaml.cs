using MahApps.Metro.Controls;
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

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for SearchScreen.xaml
	/// </summary>
	public partial class SearchScreen : MetroWindow
	{
		public SearchScreen()
		{
			InitializeComponent();
		}
		private void LaunchGitHubSite(object sender, RoutedEventArgs e)
		{
			// Launch the GitHub site...
		}

		private void DeployCupCakes(object sender, RoutedEventArgs e)
		{
			// deploy some CupCakes...
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
