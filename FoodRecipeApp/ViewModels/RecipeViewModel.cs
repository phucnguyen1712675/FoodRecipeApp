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

namespace FoodRecipeApp.ViewModels
{
    public class RecipeViewModel: ViewModelBase
    {
		public RecipeViewModel()
		{
			this.Recipes = DishesDataSource.Instance.AllRecipesCollection;
			this.Recipes.CollectionChanged += Recipes_CollectionChanged;
			this.FavouriteRecipes = DishesDataSource.Instance.FavouriteDishesCollection;
			this.QuoteToShow = QuotesDataSource.Instance.GetRandomQuote();
			this.ClearSelectionCommand = new DelegateCommand(this.OnClearSelectionCommandExecuted);
			this.AllRecipesPageCountTotal = this.Recipes.Count;
			this.favouriteRecipesPageCountTotal = this.FavouriteRecipes.Count;
			this.allRecipesCustomPageSize = this.allRecipesPageCountTotal > 5 ? 6 : 3;
			this.favouriteRecipesCustomPageSize = this.favouriteRecipesPageCountTotal > 5 ? 6 : 3;
		}

		private void Recipes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (Dish item in e.OldItems)
				{
					//Removed items
					item.PropertyChanged -= EntityViewModelPropertyChanged;
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (Dish item in e.NewItems)
				{
					//Added items
					item.PropertyChanged += EntityViewModelPropertyChanged;
				}
			}
		}

		public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//This will get called when the property of an object inside the collection changes
		}

		private DishesCollection _recipes;
		public DishesCollection Recipes
		{
			get => this._recipes;
			set
			{
				if (this._recipes != value)
				{
					this._recipes = value;
					RaisePropertyChanged();
				}
			}
		}

		private DishesCollection _favouriteRecipes;
		public DishesCollection FavouriteRecipes
		{
			get => this._favouriteRecipes;
			set
			{
				if (this._favouriteRecipes != value)
				{
					this._favouriteRecipes = value; 
					RaisePropertyChanged();
					this.OnPropertyChanged("FavouriteRecipesPageCountTotal");
				}
			}
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
					RaisePropertyChanged();
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
					RaisePropertyChanged();
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
					RaisePropertyChanged();
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
					RaisePropertyChanged();
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
					RaisePropertyChanged();
				}
			}
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
					RaisePropertyChanged();
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
					RaisePropertyChanged();
				}
			}
		}
	}
}