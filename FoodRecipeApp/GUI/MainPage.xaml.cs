using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using FoodRecipeApp.DTO;
using System.Windows.Media;
using Label = Telerik.Windows.Controls.Label;
using FoodRecipeApp.ViewModels;
using System.Diagnostics;
using System.Collections;
using SharpDX.Collections;
using FoodRecipeApp.DAO;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public static MainPage AppMainpage;
		public MainPage()
		{
			InitializeComponent();
			AppMainpage = this;
			this.DataContext = new RecipeViewModel();
		}
		static MainPage()
		{
			var addFavouriteItemBinding = new CommandBinding(TileViewCommandsExtension.AddNewFavouriteRecipe, OnAddFavouriteItemCommandExecute, OnCanAddFavouriteItemCommandExecute); 
			CommandManager.RegisterClassCommandBinding(typeof(RadTileViewItem), addFavouriteItemBinding); 
		}

		private static void OnCanAddFavouriteItemCommandExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private static void OnAddFavouriteItemCommandExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var tileViewItem = sender as RadTileViewItem;
			var tileView = tileViewItem.ParentTileView as RadTileView;

			if (tileViewItem == null || tileView == null || tileView.ItemsSource == null) return;

			var dataItem = tileView.ItemContainerGenerator.ItemFromContainer(tileViewItem) as Dish;
			var AllRecipesSource = AppMainpage.AllRecipesPager.Source as DishesCollection;
			var FavouriteRecipesSource = AppMainpage.FavouriteRecipesPager.Source as DishesCollection;

			if (dataItem == null || AllRecipesSource == null || FavouriteRecipesSource == null)
			{
				return;
			}
			if (dataItem.IsLove)
			{
				DishesDataSource.Instance.FavouriteDishesCollection.Add(dataItem);
			}
			else
			{
				DishesDataSource.Instance.FavouriteDishesCollection.Remove(dataItem);

				var item = DishesDataSource.Instance.AllRecipesCollection.FirstOrDefault(i => i.DishCode == dataItem.DishCode);

				if (item != null)
				{
					item.IsLove = !item.IsLove;
					if (DishDAO.Instance.updateFavouriteRecipe(dataItem.DishCode.ToString()) == 1)
					{
						MessageBox.Show("Updated");
					}
				}
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

		private void Pager_PageIndexChanging(object sender, PageIndexChangingEventArgs e)
		{
			//if (true)
			//{
			//	MessageBoxResult result = MessageBox.Show("There is unsaved data! Are you sure you want to continue?", "Confirm", MessageBoxButton.OKCancel);
			//	if (result == MessageBoxResult.Cancel)
			//	{
			//		e.Cancel = true;
			//	}
			//}
		}
	}
}
