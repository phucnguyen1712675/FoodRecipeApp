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
using System.Windows.Controls.Primitives;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		Dictionary<int, ToggleButton> toggleButtons = new Dictionary<int, ToggleButton>();

		public MainPage()
		{
			InitializeComponent();
			this.AddHandler(Tile.MouseDownEvent, new MouseButtonEventHandler(OnMouseDownEvent), true);
			/*this.radDataPager1.ItemCount = DishesDataSource.Instance.DishesCollection.Count;
			this.radTileList.ItemsSource = DishesDataSource.Instance.DishesCollection.Take(this.radDataPager1.PageSize).ToList();*/
		}

		private void OnMouseDownEvent(object sender, MouseButtonEventArgs e)
		{
			//
		}

		/*private void Example_ThemeChanged(object sender, EventArgs e)
		{
			this.Resources.MergedDictionaries.Clear();
			this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/FoodRecipeApp;component/RecipeResources.xaml", UriKind.RelativeOrAbsolute) });
		}*/

		private void radTileView_TileStateChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{
			RadTileViewItem item = e.OriginalSource as RadTileViewItem;
			if (item != null)
			{
				RadFluidContentControl fluid = item.ChildrenOfType<RadFluidContentControl>().FirstOrDefault();
				if (fluid != null)
				{
					switch (item.TileState)
					{
						case TileViewItemState.Maximized:
							fluid.State = FluidContentControlState.Large;
							break;
						case TileViewItemState.Minimized:
							fluid.State = FluidContentControlState.Normal;
							break;
						case TileViewItemState.Restored:
							fluid.State = FluidContentControlState.Normal;
							break;
						default:
							break;
					}
				}
			}
		}

		private void FavouriteButton_Click(object sender, RoutedEventArgs e)
		{
			if ((sender as ToggleButton).IsChecked.Value)
			{
				// Code for Checked state
			}
			else
			{
				// Code for Un-Checked state
			}
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
