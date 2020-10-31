using FoodRecipeApp.DTO;
using FoodRecipeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for FavouriteRecipePage.xaml
	/// </summary>
	public partial class FavouriteRecipePage : Page
	{
		static FavouriteRecipePage()
		{
			var deleteBinding = new CommandBinding(TileViewCommandsExtension.Delete, OnDeleteCommandExecute, OnCanDeleteCommandExecute);
			CommandManager.RegisterClassCommandBinding(typeof(RadTileViewItem), deleteBinding);

			var addFavouriteItemBinding = new CommandBinding(TileViewCommandsExtension.AddNewFavouriteRecipe, OnAddFavouriteItemCommandExecute, OnCanAddFavouriteItemCommandExecute);
			CommandManager.RegisterClassCommandBinding(typeof(RadTileViewItem), addFavouriteItemBinding);
		}

		public static FavouriteRecipePage AppFavouritepage;
		public FavouriteRecipePage()
		{
			InitializeComponent();
			AppFavouritepage = this;
			this.DataContext = new RecipeViewModel();
		}

		private static void OnCanDeleteCommandExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private static void OnDeleteCommandExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var tileViewItem = sender as RadTileViewItem;
			var tileView = tileViewItem.ParentTileView as RadTileView;
			if (tileViewItem == null || tileView == null) return;

			if (tileView.ItemsSource != null)
			{
				//----------------------------------------------------------------------------------------
				//var dataItem = tileView.ItemContainerGenerator.ItemFromContainer(tileViewItem) as TileInfo;
				var dataItem = tileView.ItemContainerGenerator.ItemFromContainer(tileViewItem) as Dish;

				// Note: This will change the DataContext's Items collection. 
				//var source = tileView.ItemsSource as IList;
				var source = AppFavouritepage.radDataPager1.Source as DishesCollection;
				if (dataItem != null && source != null)
				{
					source.Remove(dataItem);
				}
			}
			else
			{
				tileView.Items.Remove(tileViewItem);
			}
		}

		private static void OnCanAddFavouriteItemCommandExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private static void OnAddFavouriteItemCommandExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var tileViewItem = sender as RadTileViewItem;
			var tileView = tileViewItem.ParentTileView as RadTileView;
			if (tileViewItem == null || tileView == null) return;

			if (tileView.ItemsSource != null)
			{
				var dataItem = tileView.ItemContainerGenerator.ItemFromContainer(tileViewItem) as Dish;
				Debug.WriteLine(dataItem.DishCode);

				// Note: This will change the DataContext's Items collection. 
				var source = AppFavouritepage.radDataPager1.Source as DishesCollection;
				if (dataItem != null && source != null)
				{
					source.Remove(dataItem);
				}
			}
			else
			{
				tileView.Items.Remove(tileViewItem);
			}
		}

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
	}
}
