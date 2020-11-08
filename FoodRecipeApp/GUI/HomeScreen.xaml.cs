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
using System.Configuration;
using Telerik.Windows.Controls;
using FoodRecipeApp.DTO;
using FoodRecipeApp.DAO;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for HomeScreen.xaml
	/// </summary>
	public partial class HomeScreen : MetroWindow
	{
		public static RecipeViewModel ViewModel { get; } = new RecipeViewModel();

		public static HomeScreen AppMainpage;
		public HomeScreen()
		{
			InitializeComponent();
			AppMainpage = this;   
		}

        static HomeScreen()
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
				//Success

				if (dataItem.IsLove)
				{
					var item = ViewModel.Recipes.FirstOrDefault(i => i.DishCode == dataItem.DishCode);
					ViewModel.AddNewItemToFavouriteRecipesList(dataItem);
				}
				else
				{
					var item = ViewModel.FavouriteRecipes.FirstOrDefault(i => i.DishCode == dataItem.DishCode);
					ViewModel.RemoveItemFromFavouriteRecipesList(item);
					foreach (var tom in ViewModel.Recipes.Where(w => w.DishCode == dataItem.DishCode))
					{
						tom.IsLove = dataItem.IsLove;
					}
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
			this.Hide();
		}

		private void OpenThis()
		{
			this.Show();
		}

		private void Pager_PageIndexChanging(object sender, PageIndexChangingEventArgs e)
		{
			//Do nothing
		}

		private void VideoDishButton_Click(object sender, RoutedEventArgs e)
		{
			Button temp = (Button)sender;
			string Video = temp.Tag.ToString();
			youtubeWindow youtubeWindow = new youtubeWindow(Video);
			youtubeWindow.Show();
		}

		private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.Source is MetroAnimatedSingleRowTabControl)
			{
				//do work when tab is changed
				//UpdateKey();	
			}
		}

		private void RadFluidContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

		}

		private void MetroWindow_Closed(object sender, EventArgs e)
		{
			string AllRecipesPageSize = AllRecipesNumericUpDown.Value.ToString();
			string FavouriteRecipesPageSize = FavoriteNumericUpDown.Value.ToString();

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			if (!config.AppSettings.Settings["AllRecipesPageSize"].Value.Equals(AllRecipesPageSize))
			{
				config.AppSettings.Settings["AllRecipesPageSize"].Value = AllRecipesPageSize;
			}
			if (!config.AppSettings.Settings["FavouriteRecipesPageSize"].Value.Equals(FavouriteRecipesPageSize))
			{
				config.AppSettings.Settings["FavouriteRecipesPageSize"].Value = FavouriteRecipesPageSize;
			}
			config.Save(ConfigurationSaveMode.Minimal);
		}

		private void AddRecipeToggleButton_Click(object sender, RoutedEventArgs e)
		{
			var addRecipeWindow = new AddRecipeWindow();
			addRecipeWindow.Dying += OpenThis;
			addRecipeWindow.Show();
			this.Hide();

		}

        private void foodAutoCompleteBox_GotFocus(object sender, RoutedEventArgs e)
        {
			this.foodAutoCompleteBox.Populate(this.foodAutoCompleteBox.SearchText);
		}
    }
}
