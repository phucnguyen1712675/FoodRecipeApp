using FoodRecipeApp.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows;
using System;
using System.Windows.Threading;
using System.Linq.Dynamic;
using System.Linq;
using Telerik.Windows.Diagrams.Core;
using ICommand = System.Windows.Input.ICommand;

namespace FoodRecipeApp.ViewModels
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        public DishesCollection Recipes { get; } = new DishesCollection();
        public DishesCollection FavouriteRecipes { get; } = new DishesCollection();
        public string QuoteToShow => QuotesDataSource.Instance.GetRandomQuote();
        public ICommand ClearSelectionCommand { get; set; }
        public bool IsClearButtonVisible => !string.IsNullOrEmpty(SearchText);
        public bool IsDropDownOpen { get; set; }
        public Dish SelectedSearchItem { get; set; }
        public int AllRecipesPageSize { get; set; }
        public int FavouriteRecipesPageSize { get; set; }

        public int SetSortIndex { get; set; }

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

        public ObservableCollection<OrderedMethod> OrderedList { get; } = new ObservableCollection<OrderedMethod>()
            {
                new OrderedMethod("Default"),
                new OrderedMethod("Ascending Ordered By Name"),
                new OrderedMethod("Descending Ordered By Name"),
                new OrderedMethod("Ascending Ordered By Date"),
                new OrderedMethod("Descending Ordered By Date")
            };

        public Dictionary<string, List<string>> TypeAndIngredientCollection { get; set; }
        public List<string> CookingMethodCollection { get; } = new List<string>()
            {
                "Chiên",
                "Nướng",
                "Lên men",
                "Xào",
                "Kho",
                "Hấp",
                "Khác"
            };

#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67
        public DishesCollection allRecipeBeforeSearch { get;}
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RecipeViewModel()
        {
            foreach (var item in DishesCollection.GetAllDishes())
            {
                this.Recipes.Add(item);
                if(item.IsLove)
                {
                    this.FavouriteRecipes.Add(item);
                }
            }

            allRecipeBeforeSearch = DishesCollection.GetAllDishes();
            this.ClearSelectionCommand = new DelegateCommand(this.OnClearSelectionCommandExecuted);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var allPageSize = int.Parse(config.AppSettings.Settings["AllRecipesPageSize"].Value);
            var favPageSize = int.Parse(config.AppSettings.Settings["FavouriteRecipesPageSize"].Value);
            var defaultPageSize = ColumnsCount * RowsCount;
            this.AllRecipesPageSize = allPageSize != 0 ? allPageSize : (this.Recipes.Count >= defaultPageSize ? defaultPageSize : this.Recipes.Count);
            this.FavouriteRecipesPageSize = favPageSize != 0 ? favPageSize : (this.FavouriteRecipes.Count >= defaultPageSize ? defaultPageSize : this.FavouriteRecipes.Count);
            this.TypeAndIngredientCollection = new Dictionary<string, List<string>>();
            this.TypeAndIngredientCollection.Add("Tất cả", new List<string>() { });
            this.TypeAndIngredientCollection.Add("Mặn", new List<string>() { "Heo", "Gà", "Bò", "Dê", "Hải sản", "Khác" });
            this.TypeAndIngredientCollection.Add("Chay", new List<string>() { });
            this.TypeAndIngredientCollection["Tất cả"] = this.TypeAndIngredientCollection["Mặn"].Concat(this.TypeAndIngredientCollection["Chay"]).ToList();
            this.SetSortIndex = int.Parse(config.AppSettings.Settings["SetSort"].Value);
            setSort(OrderedList[SetSortIndex].Method, this.Recipes);
            setSort(OrderedList[SetSortIndex].Method, this.FavouriteRecipes);
        }

        #region search

        public void SearchPaging(List<Dish> result)
        {
            this.Recipes.Clear();
            this.FavouriteRecipes.Clear();
            foreach (var item in result)
            {
                this.Recipes.Add(item);
                if (item.IsLove)
                {
                    this.FavouriteRecipes.Add(item);
                }
            }
        }

        public List<Dish> SearchPagingByTextBox(string str, List<Dish> source)
        {
            str = Dish.RemoveDiacritics(str);
            string NameQuery = Dish.CreateQueryLinQ(str, "item");
            //TODO
            Dictionary<string, Dish> tempNames = new Dictionary<string, Dish>();
            foreach(var dish in source)
            {
                tempNames.Add(Dish.RemoveDiacritics(dish.Name) + " " + dish.DishCode.ToString(), dish);
            }

            List<string> resultNames = new List<string>();

            if(Dish.checkQuery(NameQuery))
               resultNames = tempNames.Keys.ToList().WhereDynamic(item => NameQuery).ToList();

            List<Dish> resultDishes = new List<Dish>();

            foreach(string name in resultNames)
            {
                resultDishes.Add(tempNames[name]);
            }

            return resultDishes;
        }

        public List<Dish> SearchPagingByTextBoxOnly(string str)
        {
            return SearchPagingByTextBox(str, allRecipeBeforeSearch.ToList());
        }

        public List<Dish> SearchPagingByTextBoxWithFilters(string str, List<Dish> filterRecipes)
        {
            return SearchPagingByTextBox(str, filterRecipes);
        }

        public List<Dish> SearchPagingByDishCode(int DishCode)
        {
            return allRecipeBeforeSearch.Where(item => item.DishCode == DishCode).ToList();
        }

        public List<Dish> getAll()
        {
            return allRecipeBeforeSearch.ToList();
        }

        #endregion

        public bool AddNewItemToAllRecipesList(Dish newDish)
        {
            bool result = false;

            if (newDish != null)
            {
                result = true;
                this.Recipes.Add(newDish);
                this.allRecipeBeforeSearch.Add(newDish);
            }
            setSort(OrderedList[SetSortIndex].Method, this.Recipes);
            setSort(OrderedList[SetSortIndex].Method, this.FavouriteRecipes);
            return result;
        }

        public bool RemoveItemToAllRecipesList(Dish deletedDish)
        {
            bool result = false;

            if (deletedDish != null)
            {
                result = true;
                this.Recipes.Remove(deletedDish);
                this.FavouriteRecipes.Remove(deletedDish);
                this.allRecipeBeforeSearch.Remove(deletedDish);
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
           setSort(OrderedList[SetSortIndex].Method, this.FavouriteRecipes);
            return result;
        }

        public bool RemoveItemFromFavouriteRecipesList(Dish deletedDish)
        {
            bool result = false;
            if (deletedDish != null)
            {
                result = true;
                try
                {
                    this.FavouriteRecipes.Remove(deletedDish);
                }
                catch(Exception e)
                {
                }
            }
            return result;
        }

        private void OnClearSelectionCommandExecuted(object obj)
        {
            this.SearchText = string.Empty;
            this.SelectedSearchItem = null;
            this.IsDropDownOpen = false;
        }

        public void setSort (string method , DishesCollection dishes)
        {
            dishes.SetSort(method);
            allRecipeBeforeSearch.SetSort(method);
        }

        public void FilterRecipesCollection(string ThingToFilter)
        {
            this.Recipes.Filtering(ThingToFilter);
            this.FavouriteRecipes.Filtering(ThingToFilter);
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