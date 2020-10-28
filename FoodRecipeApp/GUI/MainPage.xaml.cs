using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using FoodRecipeApp.ViewModels;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using System.Diagnostics;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
			/*this.radDataPager1.ItemCount = DishesDataSource.Instance.DishesCollection.Count;
			this.radTileList.ItemsSource = DishesDataSource.Instance.DishesCollection.Take(this.radDataPager1.PageSize).ToList();*/
		}

		private void Example_ThemeChanged(object sender, EventArgs e)
		{
			this.Resources.MergedDictionaries.Clear();
			this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/FoodRecipeApp;component/RecipeResources.xaml", UriKind.RelativeOrAbsolute) });
		}

		/*private void radDataPager1_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
		{
			if (DishesDataSource.Instance.DishesCollection != null)
			{
				this.radTileList.ItemsSource = DishesDataSource.Instance.DishesCollection.Skip(e.NewPageIndex * this.radDataPager1.PageSize).Take(this.radDataPager1.PageSize).ToList();
			}
		}*/
	}
}
