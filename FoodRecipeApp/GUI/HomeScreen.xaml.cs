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
using System.Linq.Dynamic;
using System.Collections.Specialized;

namespace FoodRecipeApp.GUI
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : MetroWindow
    {
        //TODO
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
                string UpdateDate = Dish.getUpdateDateByDishCode(dataItem.DishCode);
                //Success
                if (dataItem.IsLove)
                {
                    var item = ViewModel.Recipes.FirstOrDefault(i => i.DishCode == dataItem.DishCode);
                    dataItem.DateCreate = UpdateDate;
                    ViewModel.AddNewItemToFavouriteRecipesList(dataItem);
                }
                else
                {
                    var item = ViewModel.FavouriteRecipes.FirstOrDefault(i => i.DishCode == dataItem.DishCode);
                    ViewModel.RemoveItemFromFavouriteRecipesList(item);
                }
                foreach (var tom in ViewModel.Recipes.Where(w => w.DishCode == dataItem.DishCode))
                {
                    tom.IsLove = dataItem.IsLove;
                    tom.DateCreate = UpdateDate;
                }

                // ViewModel.updateCreateDateToNow(dataItem);
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
            if (e.Source is MetroAnimatedSingleRowTabControl)
            {
                if (DiscoverTabItem.IsSelected)
                {
                    SearchBar.Visibility = Visibility.Collapsed;
                    FilterButton.HorizontalAlignment = HorizontalAlignment.Right;
                }
                else
                {
                    SearchBar.Visibility = Visibility.Visible;
                }
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

            this.TypeOfRecipesListBox.SelectedItem = this.TypeOfRecipesListBox.Items[0];
            ViewModel.Recipes.CollectionChanged += Recipes_CollectionChanged;
            ViewModel.FavouriteRecipes.CollectionChanged += FavouriteRecipes_CollectionChanged;
        }

        private void FavouriteRecipes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            bool isEmpty = !ViewModel.FavouriteRecipes.Any();

            if (isEmpty)
            {
                FavouriteRecipesTileView.Visibility = Visibility.Collapsed;
                FavouriteRecipesPager.Visibility = Visibility.Collapsed;
                EmptyFavRecipesLabel.Visibility = Visibility.Visible;
            }
            else
            {
                if (FavouriteRecipesTileView.Visibility == Visibility.Collapsed)
                {
                    FavouriteRecipesTileView.Visibility = Visibility.Visible;
                }
                if (FavouriteRecipesPager.Visibility == Visibility.Collapsed)
                {
                    FavouriteRecipesPager.Visibility = Visibility.Visible;
                }
                if (EmptyFavRecipesLabel.Visibility == Visibility.Visible)
                {
                    EmptyFavRecipesLabel.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Recipes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            bool isEmpty = !ViewModel.Recipes.Any();

            if (isEmpty)
            {
                AllRecipesTileView.Visibility = Visibility.Collapsed;
                AllRecipesPager.Visibility = Visibility.Collapsed;
                EmptyAllRecipesLabel.Visibility = Visibility.Visible;
            }
            else
            {
                if (AllRecipesTileView.Visibility == Visibility.Collapsed)
                {
                    AllRecipesTileView.Visibility = Visibility.Visible;
                }
                if (AllRecipesPager.Visibility == Visibility.Collapsed)
                {
                    AllRecipesPager.Visibility = Visibility.Visible;
                }
                if (EmptyAllRecipesLabel.Visibility == Visibility.Visible)
                {
                    EmptyAllRecipesLabel.Visibility = Visibility.Collapsed;
                }
            }
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

//search
        private void foodAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemp = ViewModel.SelectedSearchItem;
            if (itemp == null) return;
            int dishCode = itemp.DishCode;
            Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 1));
            ViewModel.SearchPaging(ViewModel.SearchPagingByDishCode(dishCode));
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

        //search
        private void SearchDishNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchDishNameTextBox.Text.TrimStart()))
            {
                HintSearchDishNameTextBlock.Visibility = Visibility.Visible;
                XSearchDishNameImage.Visibility = Visibility.Hidden;
                ViewModel.SearchPaging(ViewModel.getAll());
            }
            else
            {
                HintSearchDishNameTextBlock.Visibility = Visibility.Hidden;
                XSearchDishNameImage.Visibility = Visibility.Visible;
                //TODO search recipes by Name and types and by sort
                //ViewModel.SearchPaging(Dish.AdvanceSearch(SearchDishNameTextBox.Text, ""));
                ViewModel.SearchPaging(ViewModel.SearchPagingByTextBox(SearchDishNameTextBox.Text));
            }
        }

        private void XSearchDishNameImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchDishNameTextBox.Text = "";
        }

        private void SplitButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = this.SplitButton.SelectedItem as OrderedMethod;
            ViewModel.setSort(selectedItem.Method, ViewModel.Recipes);
            ViewModel.setSort(selectedItem.Method, ViewModel.FavouriteRecipes);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["SetSort"].Value = SplitButton.SelectedIndex.ToString();
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.SearchPaging(ViewModel.getAll());
        }

        private int LastFilterItemNum = 0;
        private void FilterChipListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var FilterList = (sender as ListBox).SelectedItems;

            if (FilterList.Count < LastFilterItemNum)
            {
                ViewModel.getAll();
            }
            LastFilterItemNum = FilterList.Count;

            if (!FilterList.Any()) return;

            foreach (var item in FilterList)
            {
                ViewModel.FilterRecipesCollection(item.ToString());
            }
        }

        private void ChoiceChipListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Filter = (sender as ListBox).SelectedItem.ToString();
            var index = (sender as ListBox).SelectedIndex;

            this.IngredientListBox.ItemsSource = ViewModel.TypeAndIngredientCollection[Filter];

            ViewModel.getAll();
            if (index != 0)
            {
                ViewModel.FilterRecipesCollection(Filter);
            }
        }
    }
}
