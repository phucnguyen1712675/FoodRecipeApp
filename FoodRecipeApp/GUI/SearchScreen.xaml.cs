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
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using FoodRecipeApp.DAO;
using FoodRecipeApp.ViewModels;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for SearchScreen.xaml
	/// </summary>
	public partial class SearchScreen : Page
	{
		public static SearchScreen SearchPage;

		CheckedList ListCheckBoxes
        {
			get;
			set;
        }
		public DishesCollection FilterList;
		public DishesCollection FullList;
		public SearchScreen()
		{
			InitializeComponent();

			AddAllCheckBox();
			FullList = DishesCollection.GetAllDishes();
			ShowAll();
			SearchPage = this;
			var viewModel = new RecipeViewModel();
			this.DataContext = viewModel;

			// Getting the currently selected ListBoxItem
			// Note that the ListBox must have
			// IsSynchronizedWithCurrentItem set to True for this to work
		}
		public int AddAllCheckBox()
        {
			List<CheckBox> ListABC = new List<CheckBox>
			{
				checkBox1,
				checkBox2,
				checkBox3,
				checkBox4,
				checkBox5,
				checkBox6,
				checkBox7,
				checkBox8,
				checkBox9,
				//ListABC.Add(checkBox10);
				checkBox11,
				checkBox12,
				checkBox13,
				checkBox14,
				checkBox15,
				checkBox16,
				checkBox17,
				checkBox18
			};
			ListCheckBoxes = new CheckedList(ListABC);
			return 0;
        }
		public void ShowAll()
        {
			this.DataContext = FullList;
        }
		public void ShowSearchResult()
        {
			this.DataContext = this.FilterList;
		}
		private void Check_Click(object sender, RoutedEventArgs e)
		{
			string FilterQuery = ListCheckBoxes.GetFilterQuery();
			this.FilterList = DishesCollection.GetFilterDishes(FilterQuery);
			ShowSearchResult();
		}
		private void Search_Click(object sender, RoutedEventArgs e)
        {
			string NameSearch = SearchBox.Text;
			this.FilterList = DishesCollection.GetDishByName(NameSearch);
			ShowSearchResult();
		}
        private void Unchecked_Click(object sender, RoutedEventArgs e)
        {
			ShowAll();
			SearchBox.Clear();
			ListCheckBoxes.UncheckAll();
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

				var viewModel = (RecipeViewModel)SearchPage.DataContext;
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

		private void VideoDishButton_Click(object sender, RoutedEventArgs e)
		{
			Button temp = (Button)sender;
			string Video = temp.Tag.ToString();
			youtubeWindow youtubeWindow = new youtubeWindow(Video);
			youtubeWindow.Show();
		}

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
			string NameSearch = SearchBox.Text;
			this.FilterList = DishesCollection.GetDishByName(NameSearch);
			ShowSearchResult();
		}
    }

}
