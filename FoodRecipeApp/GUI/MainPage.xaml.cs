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
			var viewModel = new RecipeViewModel();
			this.DataContext = viewModel;
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

			if (dataItem == null) return;

			if (DishDAO.Instance.updateFavouriteRecipe(dataItem.DishCode.ToString()) == 1)
			{
				MessageBox.Show("Updated");

				var viewModel = (RecipeViewModel)AppMainpage.DataContext;
				if (dataItem.IsLove)
				{
					var item = viewModel.Recipes.FirstOrDefault(i => i.DishCode == dataItem.DishCode);
					viewModel.AddNewItemToFavouriteRecipesList(dataItem);
				}
				else
				{
					var item = viewModel.FavouriteRecipes.FirstOrDefault(i => i.DishCode == dataItem.DishCode);
					viewModel.RemoveItemFromFavouriteRecipesList(item);
					foreach (var tom in viewModel.Recipes.Where(w => w.DishCode == dataItem.DishCode))
					{
						tom.IsLove = dataItem.IsLove;
					}
				}
				if (!viewModel.UpdateFavouriteRecipesCustomPageSize())
				{
					MessageBox.Show("Error");
				}
			}
		}

		private void radTileView_TileStateChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{
			RadTileViewItem item = e.OriginalSource as RadTileViewItem;

			if (item == null) return;

			RadFluidContentControl fluid = item.ChildrenOfType<RadFluidContentControl>().FirstOrDefault();

			if (fluid == null) return;

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

		private void Pager_PageIndexChanging(object sender, PageIndexChangingEventArgs e)
		{
			//Do nothing
		}
	}
}
