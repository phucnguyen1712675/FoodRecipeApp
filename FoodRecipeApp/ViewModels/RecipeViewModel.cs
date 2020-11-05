using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using FoodRecipeApp.GUI;
using System.Windows.Input;
using Telerik.Windows.Controls.Slider;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;
using PropertyChanged;

namespace FoodRecipeApp.ViewModels
{
    public class RecipeViewModel: ViewModelBase, INotifyPropertyChanged
    {
		private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
		///public DelegateCommand AddItems { get; }
		//public DelegateCommand AddItems { get; }
		
		public RecipeViewModel()
		{
			/*AddItems = new DelegateCommand(() => Task.Run(() =>
				Parallel.ForEach(DishesCollection.GetAllDishes(),
					 (item) => dispatcher.Invoke(() => Recipes.Add(item))))
			);*/

			/*Items.Add(new Item() { ItemId = 1 });
			for (int i = 1; i < 10; i++)
			{
				Items.Add(new Item() { ItemId = i });
			}
			Recipes = DishesCollection.GetAllDishes();*/
			this.ModifiedItems = new List<Dish>();

			foreach (var item in DishesCollection.GetAllDishes()) this.Recipes.Add(item);
			this.Recipes.CollectionChanged += this.OnCollectionChanged;

			foreach (var item in DishesCollection.GetFavouriteDishes()) this.FavouriteRecipes.Add(item);
			this.FavouriteRecipes.CollectionChanged += this.OnCollectionChanged;

			this.QuoteToShow = QuotesDataSource.Instance.GetRandomQuote();
			this.ClearSelectionCommand = new DelegateCommand(this.OnClearSelectionCommandExecuted);
			this.AllRecipesPageCountTotal = this.Recipes.Count;
			this.favouriteRecipesPageCountTotal = this.FavouriteRecipes.Count;
			this.allRecipesCustomPageSize = this.allRecipesPageCountTotal > 5 ? 6 : 3;
			this.favouriteRecipesCustomPageSize = this.favouriteRecipesPageCountTotal > 6 ? 6 : this.favouriteRecipesPageCountTotal;
		}

		[SuppressPropertyChangedWarnings]
		void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				foreach (Dish newItem in e.NewItems)
				{
					ModifiedItems.Add(newItem);

					//Add listener for each item on PropertyChanged event
					newItem.PropertyChanged += this.OnItemPropertyChanged;
				}
			}

			if (e.OldItems != null)
			{
				foreach (Dish oldItem in e.OldItems)
				{
					ModifiedItems.Add(oldItem);

					oldItem.PropertyChanged -= this.OnItemPropertyChanged;
				}
			}
		}

		[SuppressPropertyChangedWarnings]
		void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Dish item = sender as Dish;
			if (item != null)
				ModifiedItems.Add(item);
		}

		public List<Dish> ModifiedItems { get; set; }


		public DishesCollection Recipes { get; } = new DishesCollection();
		public bool AddNewItemToAllRecipesList(Dish newDish)
		{
			bool result = false;

			if (newDish != null)
			{
				result = true;
				this.Recipes.Add(newDish);
			}
			//TODO
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

		public DishesCollection FavouriteRecipes { get; } = new DishesCollection();

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

		public string QuoteToShow { get; set; }

		public ICommand ClearSelectionCommand { get; set; }

		public bool IsClearButtonVisible => !string.IsNullOrEmpty(SearchText);

		private bool isDropDownOpen;
		public bool IsDropDownOpen
		{
			get => this.isDropDownOpen;
			set
			{
				if (this.isDropDownOpen != value)
				{
					this.isDropDownOpen = value;
					RaisePropertyChanged("IsDropDownOpen");
				}
			}
		}

		private Dish selectedSearchItem;
		public Dish SelectedSearchItem
		{
			get => this.selectedSearchItem;
			set
			{
				if (this.selectedSearchItem != value)
				{
					this.selectedSearchItem = value;
					RaisePropertyChanged("SelectedSearchItem");
				}
			}
		}

		private string searchText;
		public string SearchText
		{
			get => this.searchText;
			set
			{
				if (searchText != value)
				{
					this.searchText = value;
					this.OnPropertyChanged("IsClearButtonVisible");
					RaisePropertyChanged("SearchText");
				}
			}
		}

		private void OnClearSelectionCommandExecuted(object obj)
		{
			this.SearchText = string.Empty;
			this.SelectedSearchItem = null;
			this.IsDropDownOpen = false;
		}

		private int allRecipesCustomPageSize;
		public int AllRecipesCustomPageSize
		{
			get => this.allRecipesCustomPageSize;
			set
			{
				if (this.allRecipesCustomPageSize != value)
				{
					this.allRecipesCustomPageSize = value;
					RaisePropertyChanged("AllRecipesCustomPageSize");
				}
			}
		}

		private int favouriteRecipesCustomPageSize;
		public int FavouriteRecipesCustomPageSize
		{
			get => this.favouriteRecipesCustomPageSize;
			set
			{
				if (this.favouriteRecipesCustomPageSize != value)
				{
					this.favouriteRecipesCustomPageSize = value;
					RaisePropertyChanged("FavouriteRecipesCustomPageSize");
				}
			}
		}

		public bool UpdateFavouriteRecipesCustomPageSize()
		{
			bool flag = true;

			if (this.favouriteRecipesPageCountTotal <  7)
			{
				this.favouriteRecipesCustomPageSize = this.FavouriteRecipes.Count;
			}
			else
			{
				this.favouriteRecipesCustomPageSize = 6;
				flag = false;
			}

			return flag;
		}

		private int allRecipesPageCountTotal;
		public int AllRecipesPageCountTotal
		{
			get => this.allRecipesPageCountTotal;
			set
			{
				if (this.allRecipesPageCountTotal != value)
				{
					this.allRecipesPageCountTotal = value;
					RaisePropertyChanged("AllRecipesPageCountTotal");
				}
			}
		}

		private int favouriteRecipesPageCountTotal;
		public int FavouriteRecipesPageCountTotal
		{
			get => this.FavouriteRecipes.Count;
			set
			{
				if (this.favouriteRecipesPageCountTotal != value)
				{
					this.favouriteRecipesPageCountTotal = value;
					RaisePropertyChanged("FavouriteRecipesPageCountTotal");
				}
			}
		}

		/*private ICommand mUpdater;
		public ICommand UpdateCommand
		{
			get
			{
				if (mUpdater == null)
					mUpdater = new Updater();
				return mUpdater;
			}
			set
			{
				mUpdater = value;
			}
		}

		private class Updater : ICommand
		{
			#region ICommand Members  

			public bool CanExecute(object parameter)
			{
				return true;
			}

			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{

			}

			#endregion
		}*/
	}
}