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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using FoodRecipeApp.ViewModels;
using MenuItem = FoodRecipeApp.ViewModels.MenuItem;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for HomeScreen.xaml
	/// </summary>
	public partial class HomeScreen : MetroWindow
	{
        public static HomeScreen homeScreen;

		private readonly Navigation.NavigationServiceEx _navigationServiceEx;
		public HomeScreen()
		{
            homeScreen = this;
			InitializeComponent();
            this._navigationServiceEx = new Navigation.NavigationServiceEx();
            this._navigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
            this.HamburgerMenuControl.Content = this._navigationServiceEx.Frame;

            // Navigate to the home page.
            this.Loaded += (sender, args) => this._navigationServiceEx.Navigate(new Uri("GUI/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                this._navigationServiceEx.Navigate(menuItem.NavigationDestination);
            }
        }

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {
            // select the menu item
            this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
                                                         .Items
                                                         .OfType<MenuItem>()
                                                         .FirstOrDefault(x => x.NavigationDestination == e.Uri);
            this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
                                                                .OptionsItems
                                                                .OfType<MenuItem>()
                                                                .FirstOrDefault(x => x.NavigationDestination == e.Uri);

            // or when using the NavigationType on menu item
            // this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
            //                                              .Items
            //                                              .OfType<MenuItem>()
            //                                              .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());
            // this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
            //                                                     .OptionsItems
            //                                                     .OfType<MenuItem>()
            //                                                     .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());

            // update back button
            this.GoBackButton.Visibility = this._navigationServiceEx.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GoBackButton_OnClick(object sender, RoutedEventArgs e)
		{
			this._navigationServiceEx.GoBack();
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
