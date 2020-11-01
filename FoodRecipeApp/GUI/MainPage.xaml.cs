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

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		static MainPage()
		{
			var deleteBinding = new CommandBinding(TileViewCommandsExtension.Delete, OnDeleteCommandExecute, OnCanDeleteCommandExecute);
			CommandManager.RegisterClassCommandBinding(typeof(RadTileViewItem), deleteBinding);

			var addFavouriteItemBinding = new CommandBinding(TileViewCommandsExtension.AddNewFavouriteRecipe, OnAddFavouriteItemCommandExecute, OnCanAddFavouriteItemCommandExecute); 
			CommandManager.RegisterClassCommandBinding(typeof(RadTileViewItem), addFavouriteItemBinding); 
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
				var source = AppMainpage.radDataPager1.Source as DishesCollection;
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
				var source = AppMainpage.radDataPager1.Source as DishesCollection;
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

		public static MainPage AppMainpage; 

		public MainPage()
		{
			InitializeComponent();
			AppMainpage = this;
			this.DataContext = new RecipeViewModel();
			//this.AddHandler(Tile.MouseDownEvent, new MouseButtonEventHandler(OnMouseDownEvent), true);
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

		private void ToggleButton_Click(object sender, RoutedEventArgs e)
		{
			ToggleButton Temp = (ToggleButton)sender;
			int dishCode = (int)(Temp.Tag);
			if (Dish.updateIsLoveDish(dishCode) == 1)
			{
				MessageBox.Show("Update thành công");
			}
			else
			{
				MessageBox.Show("Update thất bại");
			}
		}

		private void DetailSteps_Click(object sender, RoutedEventArgs e)
		{
			Button temp = (Button)sender;
			int DishCode = (int)(temp.Tag);
			List<Step> steps = Step.getAllStepsInDish(DishCode);
			var addedStepsScreen = new AddedStepWindow(steps);
			addedStepsScreen.Dying += OpenThis;
			addedStepsScreen.Show();
			HomeScreen.homeScreen.Hide();
		}

		private void OpenThis()
		{
			HomeScreen.homeScreen.Show();
		}
	}
}
