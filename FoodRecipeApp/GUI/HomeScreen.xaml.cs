using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using FoodRecipeApp.ViewModels;
using System.Configuration;
using Telerik.Windows.Controls;
using FoodRecipeApp.DTO;
using FoodRecipeApp.DAO;
using System;
using System.Diagnostics;

namespace FoodRecipeApp.GUI
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : MetroWindow
	{
		//TODO
		public static RecipeViewModel ViewModel { get; set; } = new RecipeViewModel();

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
			var item = e.OriginalSource as RadTileViewItem;

			if (item == null) return;

			RadFluidContentControl fluid = item.ChildrenOfType<RadFluidContentControl>().FirstOrDefault();

			if (fluid == null) return;

            switch (item.TileState)
            {
				case TileViewItemState.Maximized:
					fluid.State = FluidContentControlState.Large;
					this.AddRecipeToggleButton.Visibility = Visibility.Hidden;
					break;
				case TileViewItemState.Minimized:
					fluid.State = FluidContentControlState.Normal;
					break;
				case TileViewItemState.Restored:
					fluid.State = FluidContentControlState.Normal;
					this.AddRecipeToggleButton.Visibility = Visibility.Visible;
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

		private void VideoDishButton_Click(object sender, RoutedEventArgs e)
		{
			Button temp = (Button)sender;
			string Video = temp.Tag.ToString();
			youtubeWindow youtubeWindow = new youtubeWindow(Video);
			youtubeWindow.Show();
		}

		private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

    	if (DiscoverTabItem.IsSelected) SearchBar.Visibility = Visibility.Hidden;
			else { 
					SearchBar.Visibility = Visibility.Visible;
					SearchDishNameTextBox.Text = foodAutoCompleteBox.SearchText;
		  }
    }

		private void AddRecipeToggleButton_Click(object sender, RoutedEventArgs e)
		{
			var addRecipeWindow = new AddRecipeWindow();
			addRecipeWindow.Dying += OpenThis;
			addRecipeWindow.Show();
			this.Hide();
		}

        private void showSplashScreenToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;

			if (toggleSwitch != null)
			{
				var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				config.AppSettings.Settings["ShowSplashScreen"].Value = toggleSwitch.IsOn.ToString();
				config.Save(ConfigurationSaveMode.Minimal);
			}
		}

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			this.showSplashScreenToggleSwitch.IsOn = bool.Parse(config.AppSettings.Settings["ShowSplashScreen"].Value);
        }

        private void AllRecipesNumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
			var AllRecipesPageSize = AllRecipesNumericUpDown.Value;

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings["AllRecipesPageSize"].Value = AllRecipesPageSize.ToString();
			config.Save(ConfigurationSaveMode.Minimal);
		}

        private void FavoriteNumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
			var FavouriteRecipesPageSize = FavoriteNumericUpDown.Value;

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings["FavouriteRecipesPageSize"].Value = FavouriteRecipesPageSize.ToString();
			config.Save(ConfigurationSaveMode.Minimal);
		}

        private void foodAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var itemp = ViewModel.SelectedSearchItem;
			if (itemp == null) return;
			Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 1));
			Dispatcher.BeginInvoke((Action)(() => AllRecipesPager.PageIndex = (itemp.DishCode - 1) / ViewModel.AllRecipesPageSize));
			//().ParentOfType<RadTileViewItem>().TileState = TileViewItemState.Maximized;
			this.AllRecipesTileView.MaximizedItem = this.AllRecipesTileView.Items[(itemp.DishCode - 1) % ViewModel.AllRecipesPageSize];

			RadTileViewItem maximizedItem = this.AllRecipesTileView.ItemContainerGenerator.ContainerFromItem(this.AllRecipesTileView.MaximizedItem) as RadTileViewItem;
            if (maximizedItem == null)
            {
                MessageBox.Show("You have to maximize an item first.");
                return;
            }
            RadFluidContentControl fluidContentControl = maximizedItem.FindChildByType<RadFluidContentControl>();
        }

		private void RadFluidContentControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
			var image = sender as FrameworkElement;
			if (image == null) return;

			var container = image.ParentOfType<RadTileViewItem>();
			if (container != null)
			{
				container.TileState = container.TileState != TileViewItemState.Maximized ? TileViewItemState.Maximized : TileViewItemState.Restored;
			}
		}

         private void SearchDishNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
			      if (SearchDishNameTextBox.Text.Length == 0)
            {
              HintSearchDishNameTextBlock.Visibility = Visibility.Visible;
              XSearchDishNameImage.Visibility = Visibility.Hidden;
              ViewModel = new RecipeViewModel();
            }
            else
            {
              HintSearchDishNameTextBlock.Visibility = Visibility.Hidden;
              XSearchDishNameImage.Visibility = Visibility.Visible;
              //TODO search recipes
              ViewModel = new RecipeViewModel(Dish.AdvanceSearch(SearchDishNameTextBox.Text, ""));
              this.DataContext = ViewModel;
            }
		    }
        
        private void XSearchDishNameImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
			    SearchDishNameTextBox.Text = "";
        }
    }
}
