﻿using FoodRecipeApp.DTO;
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
using System.Configuration;

namespace FoodRecipeApp.ViewModels
{
    public class RecipeViewModel: ViewModelBase, INotifyPropertyChanged
    {
		public List<Dish> ModifiedItems { get; set; }
		public DishesCollection Recipes { get; } = new DishesCollection();
		public DishesCollection FavouriteRecipes { get; } = new DishesCollection();
		public BindingList<Dish> SearchRecipes { get; set; }
		public string QuoteToShow => QuotesDataSource.Instance.GetRandomQuote();
		public ICommand ClearSelectionCommand { get; set; }
		public bool IsClearButtonVisible => !string.IsNullOrEmpty(SearchText);
		public bool IsDropDownOpen { get; set; }
		public Dish SelectedSearchItem { get; set; }
		public string SearchText { get; set; }
		public int AllRecipesPageSize { get; set; }
		public int FavouriteRecipesPageSize { get; set; }

		public RecipeViewModel()
		{
			this.ModifiedItems = new List<Dish>();

			foreach (var item in DishesCollection.GetAllDishes()) this.Recipes.Add(item);
			this.Recipes.CollectionChanged += this.OnCollectionChanged;

			foreach (var item in DishesCollection.GetFavouriteDishes()) this.FavouriteRecipes.Add(item);
			this.FavouriteRecipes.CollectionChanged += this.OnCollectionChanged;

			this.ClearSelectionCommand = new DelegateCommand(this.OnClearSelectionCommandExecuted);

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			this.AllRecipesPageSize = int.Parse(config.AppSettings.Settings["AllRecipesPageSize"].Value);
			int favPageSize = int.Parse(config.AppSettings.Settings["FavouriteRecipesPageSize"].Value);
			if (favPageSize != 0)
			{
				this.FavouriteRecipesPageSize = favPageSize;
			}
			else
			{
				int favItemCount = this.FavouriteRecipes.Count;
				if (favItemCount < 7)
				{
					this.FavouriteRecipesPageSize = favItemCount;
				}
				else
				{
					this.FavouriteRecipesPageSize = 6;
				}
			}			

			this.SearchRecipes = new BindingList<Dish>(this.Recipes);
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

		public bool AddNewItemToAllRecipesList(Dish newDish)
		{
			bool result = false;

			if (newDish != null)
			{
				result = true;
				this.ModifiedItems.Add(newDish);
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

			this.SearchRecipes.Clear();
			//string FilterQuery = ListCheckBoxes.GetFilterQuery();
			//this.SearchRecipes = DishesCollection.GetFilterDishes(FilterQuery);

			return result;
		}




		//private bool isDropDownOpen;
		//{
		//	get => this.isDropDownOpen;
		//	set
		//	{
		//		if (this.isDropDownOpen != value)
		//		{
		//			this.isDropDownOpen = value;
		//			RaisePropertyChanged("IsDropDownOpen");
		//		}
		//	}
		//}

		//private Dish selectedSearchItem;
		//{
		//	get => this.selectedSearchItem;
		//	set
		//	{
		//		if (this.selectedSearchItem != value)
		//		{
		//			this.selectedSearchItem = value;
		//			RaisePropertyChanged("SelectedSearchItem");
		//		}
		//	}
		//}

		//private string searchText;
		//{
		//	get => this.searchText;
		//	set
		//	{
		//		if (searchText != value)
		//		{
		//			this.searchText = value;
		//			this.OnPropertyChanged("IsClearButtonVisible");
		//			RaisePropertyChanged("SearchText");
		//		}
		//	}
		//}

		//private int allRecipesCustomPageSize;
		//{
		//	get => this.allRecipesCustomPageSize;
		//	set
		//	{
		//		if (this.allRecipesCustomPageSize != value)
		//		{
		//			this.allRecipesCustomPageSize = value;
		//			RaisePropertyChanged("AllRecipesCustomPageSize");
		//		}
		//	}
		//}

		//private int favouriteRecipesCustomPageSize;
		//{
		//	get => this.favouriteRecipesCustomPageSize;
		//	set
		//	{
		//		if (this.favouriteRecipesCustomPageSize != value)
		//		{
		//			this.favouriteRecipesCustomPageSize = value;
		//			RaisePropertyChanged("FavouriteRecipesCustomPageSize");
		//		}
		//	}
		//}

		//private int allRecipesPageCountTotal;
		//{
		//	get => this.allRecipesPageCountTotal;
		//	set
		//	{
		//		if (this.allRecipesPageCountTotal != value)
		//		{
		//			this.allRecipesPageCountTotal = value;
		//			RaisePropertyChanged("AllRecipesPageCountTotal");
		//		}
		//	}
		//}

		//private int favouriteRecipesPageCountTotal;
		//{
		//	get => this.FavouriteRecipes.Count;
		//	set
		//	{
		//		if (this.favouriteRecipesPageCountTotal != value)
		//		{
		//			this.favouriteRecipesPageCountTotal = value;
		//			RaisePropertyChanged("FavouriteRecipesPageCountTotal");
		//		}
		//	}
		//}

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