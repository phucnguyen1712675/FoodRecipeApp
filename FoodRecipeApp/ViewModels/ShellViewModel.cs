using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodRecipeApp.GUI;
using FoodRecipeApp.Mvvm;
using FoodRecipeApp.Views;
using MahApps.Metro.IconPacks;

namespace FoodRecipeApp.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
        private static readonly ObservableCollection<MenuItem> AppOptionsMenu = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> Menu => AppMenu;

        public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;

        public ShellViewModel()
        {
	        // Build the menus
	        this.Menu.Add(new MenuItem()
	        {
		        Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.BugSolid},
		        Label = "Home Screen",
		        NavigationType = typeof(MainPage),
		        NavigationDestination = new Uri("GUI/MainPage.xaml", UriKind.RelativeOrAbsolute)
	        });
			this.Menu.Add(new MenuItem()
	        {
		        Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.BugSolid },
		        Label = "Search Screen",
		        NavigationType = typeof(SearchScreen),
		        NavigationDestination = new Uri("GUI/SearchScreen.xaml", UriKind.RelativeOrAbsolute)
	        });
	        this.Menu.Add(new MenuItem()
	        {
		        Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.BugSolid },
		        Label = "Add Recipe",
		        NavigationType = typeof(AddRecipe),
		        NavigationDestination = new Uri("GUI/AddRecipe.xaml", UriKind.RelativeOrAbsolute)
	        });

			this.OptionsMenu.Add(new MenuItem()
	        {
		        Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.CogsSolid},
		        Label = "Settings",
		        NavigationType = typeof(SettingsPage),
		        NavigationDestination = new Uri("GUI/SettingsPage.xaml", UriKind.RelativeOrAbsolute)
	        });
	        this.OptionsMenu.Add(new MenuItem()
	        {
		        Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.InfoCircleSolid },
		        Label = "About us",
		        NavigationType = typeof(AboutPage),
		        NavigationDestination = new Uri("GUI/AboutPage.xaml", UriKind.RelativeOrAbsolute)
	        });
        }
    }
}
