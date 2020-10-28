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
using FoodRecipeApp.DTO;
using Telerik.Windows.Controls;
using System.ComponentModel;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for SearchScreen.xaml
	/// </summary>
	public partial class SearchScreen : Page
	{
		public SearchScreen()
		{
			InitializeComponent();

			this.DataContext = Dish.GetDishes();
			this.Loaded += Example_Loaded;
			this.Unloaded += Example_Unloaded;
		}

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

		void Example_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//ApplicationThemeManager.GetInstance().ThemeChanged += this.Example_ThemeChanged;
		}

		private void Example_Unloaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//ApplicationThemeManager.GetInstance().ThemeChanged -= this.Example_ThemeChanged;
		}

		private void Example_ThemeChanged(object sender, System.EventArgs e)
		{
			this.Resources.MergedDictionaries.Clear();
			this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/TileView;component//Common/CommonTemplates.xaml", UriKind.RelativeOrAbsolute) });
			this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/TileView;component/Features/AutomaticScrolling/Resources.xaml", UriKind.RelativeOrAbsolute) });
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//this.DialogBox.Visibility = System.Windows.Visibility.Collapsed;
		}
	}

}
