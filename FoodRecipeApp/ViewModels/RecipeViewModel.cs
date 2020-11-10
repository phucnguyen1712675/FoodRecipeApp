using FoodRecipeApp.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FoodRecipeApp.ViewModels
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        public DishesCollection Recipes { get; } = new DishesCollection();
        public DishesCollection FavouriteRecipes { get; } = new DishesCollection();
        public DishesCollection SearchedRecipes { get; } = new DishesCollection();
        public string QuoteToShow => QuotesDataSource.Instance.GetRandomQuote();
        public ICommand ClearSelectionCommand { get; set; }
        public bool IsClearButtonVisible => !string.IsNullOrEmpty(SearchText);
        public bool IsDropDownOpen { get; set; }
        public Dish SelectedSearchItem { get; set; }
        public int AllRecipesPageSize { get; set; }
        public int FavouriteRecipesPageSize { get; set; }

        private string searchText;
        public string SearchText
        {
            get => this.searchText;
            set
            {
                if (searchText != value)
                {
                    this.searchText = value;
                    OnPropertyChanged("IsClearButtonVisible");
                    OnPropertyChanged("SearchText");
                }
            }
        }
        public Dish SelectedItem { get; set; }

        public const int ColumnsCount = 4;
        public const int RowsCount = 2;

        public ObservableCollection<OrderedMethod> OrderedList { get; set; }

#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RecipeViewModel()
        {
            foreach (var item in DishesCollection.GetAllDishes())
            {
                this.Recipes.Add(item);
                //this.SearchedRecipes.Add(item);
                if(item.IsLove)
                {
                    this.FavouriteRecipes.Add(item);
                }
            }

            /*this.Recipes.CollectionChanged += Recipes_CollectionChanged;
			this.FavouriteRecipes.CollectionChanged += FavouriteRecipes_CollectionChanged;*/

            this.ClearSelectionCommand = new DelegateCommand(this.OnClearSelectionCommandExecuted);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var allPageSize = int.Parse(config.AppSettings.Settings["AllRecipesPageSize"].Value);
            var favPageSize = int.Parse(config.AppSettings.Settings["FavouriteRecipesPageSize"].Value);
            var defaultPageSize = ColumnsCount * RowsCount;
            this.AllRecipesPageSize = allPageSize != 0 ? allPageSize : (this.Recipes.Count >= defaultPageSize ? defaultPageSize : this.Recipes.Count);
            this.FavouriteRecipesPageSize = favPageSize != 0 ? favPageSize : (this.FavouriteRecipes.Count >= defaultPageSize ? defaultPageSize : this.FavouriteRecipes.Count);
       
            OrderedList = new ObservableCollection<OrderedMethod>
            {
                new OrderedMethod("None"),
                new OrderedMethod("Ascending Ordered By Name"),
                new OrderedMethod("Descending Ordered By Name"),
                new OrderedMethod("Descending Ordered By Created Date"),
                new OrderedMethod("Descending Ordered By Updated Date")
            };       
       }

        public void  SearchPaging(List<Dish> objects)
        {
            this.Recipes.Clear();
            this.FavouriteRecipes.Clear();
            foreach (var item in objects)
            {
                this.Recipes.Add(item);
                this.SearchedRecipes.Add(item);
                if (item.IsLove)
                {
                    this.FavouriteRecipes.Add(item);
                }
            }
         }


        public void getAll()
        {
            foreach (var item in DishesCollection.GetAllDishes())
            {
                this.Recipes.Add(item);
                //this.SearchedRecipes.Add(item);
                if (item.IsLove)
                {
                    this.FavouriteRecipes.Add(item);
                }
            }
        }


        public bool AddNewItemToAllRecipesList(Dish newDish)
        {
            bool result = false;

            if (newDish != null)
            {
                result = true;
                this.Recipes.Add(newDish);
            }
            return result;
        }

        public bool RemoveItemToAllRecipesList(Dish deletedDish)
        {
            bool result = false;

            if (deletedDish != null)
            {
                result = true;
                this.Recipes.Remove(deletedDish);
            }
            return result;
        }


        public bool AddNewItemToFavouriteRecipesList(Dish newDish)
        {
            bool result = false;

            if (newDish != null)
            {
                result = true;
                this.FavouriteRecipes.Add(newDish);
            }

            return result;
        }

        public bool RemoveItemFromFavouriteRecipesList(Dish deletedDish)
        {
            bool result = false;

            if (deletedDish != null)
            {
                result = true;
                this.FavouriteRecipes.Remove(deletedDish);
            }

            return result;
        }

        private void OnClearSelectionCommandExecuted(object obj)
        {
            this.SearchText = string.Empty;
            this.SelectedSearchItem = null;
            this.IsDropDownOpen = false;
        }

        public bool GetNewSearchRecipes()
        {
            bool result = false;

            this.SearchedRecipes.Clear();
            //string FilterQuery = ListCheckBoxes.GetFilterQuery();
            //this.SearchRecipes = DishesCollection.GetFilterDishes(FilterQuery);

            return result;
        }

        public void SetDefaultPosition()
        {
            var count = 0;
            this.Recipes.SetDefaultPosition();

            foreach (var recipe in this.Recipes)
            {
                recipe.Position = count++;
            }
        }

        public void SetAscendingPositionAccordingToName()
        {
            var count = 0;
            this.Recipes.SetAscendingPositionAccordingToName();

            foreach (var recipe in this.Recipes)
            {
                recipe.Position = count++;
            }
        }

        public void SetDescendingPositionAccordingToName()
        {
            var count = 0;
            this.Recipes.SetDescendingPositionAccordingToName();

            foreach (var recipe in this.Recipes)
            {
                recipe.Position = count++;
            }
        }

    }

    public class OrderedMethod
    {
        public string Method { get; set; }

        public OrderedMethod(string method)
        {
            this.Method = method;
        }

    }
}