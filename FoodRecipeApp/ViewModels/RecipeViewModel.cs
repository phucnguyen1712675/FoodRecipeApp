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

namespace FoodRecipeApp.ViewModels
{
    public class RecipeViewModel: ViewModelBase
    {
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
					//this.OnPropertyChanged("Recipes");
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
					//this.OnPropertyChanged("FavouriteRecipes");
				}
			}
		}
		public string QuoteToShow { get; set; }

		private TileInfo _selectedItem;
		public TileInfo SelectedItem
		{
			get => this._selectedItem;
			set
			{
				if (this._selectedItem != value)
				{
					this._selectedItem = value;
					RaisePropertyChanged();
					//this.OnPropertyChanged("SelectedItem");
				}
			}
		}

		public ObservableCollection<TileInfo> Tiles { get; set; }

		public RecipeViewModel()
		{
			this.Recipes = DishesDataSource.Instance.DishesCollection;
			this.FavouriteRecipes = DishesDataSource.Instance.FavouriteDishesCollection;
			this.QuoteToShow = QuotesDataSource.Instance.GetRandomQuote();

			this.Tiles = new ObservableCollection<TileInfo>();
			int n = this.Recipes.Count;
			for (int i = 0; i < n; i++)
			{
				var dataItem = new TileInfo() 
				{ 
					DishCode = Recipes[i].DishCode 
				};
				this.Tiles.Add(dataItem);
			}
			this.SelectedItem = this.Tiles[0];

			this.ClearSelectionCommand = new DelegateCommand(this.OnClearSelectionCommandExecuted);
		}

		private string searchText;
		private bool isDropDownOpen;
		private Dish selectedSearchItem;

		public ICommand ClearSelectionCommand { get; set; }


		public bool IsClearButtonVisible => !string.IsNullOrEmpty(SearchText);
		public bool IsDropDownOpen
		{
			get => this.isDropDownOpen;
			set
			{
				if (this.isDropDownOpen != value)
				{
					this.isDropDownOpen = value;
					this.OnPropertyChanged("IsDropDownOpen");
				}
			}
		}

		public Dish SelectedSearchItem
		{
			get => this.selectedSearchItem;
			set
			{
				if (this.selectedSearchItem != value)
				{
					this.selectedSearchItem = value;
					this.OnPropertyChanged("SelectedItem");
				}
			}
		}

		public string SearchText
		{
			get => this.searchText;
			set
			{
				if (searchText != value)
				{
					this.searchText = value;
					this.OnPropertyChanged("IsClearButtonVisible");
					this.OnPropertyChanged("SearchText");
				}
			}
		}

		private void OnClearSelectionCommandExecuted(object obj)
		{
			this.SearchText = string.Empty;
			this.SelectedItem = null;
			this.IsDropDownOpen = false;
		}

		/*private ObservableCollection<ImagePair> items;
		private ObservableCollection<ImagePair> backgounds;
		private ImagePair selectedPair;
		private DelegateCommand selectNextPerson;
		private DelegateCommand selectPreviousPerson;

		public RecipeViewModel()
		{
			this.items = new ObservableCollection<ImagePair>();
			for (int i = 0; i < 8; i++)
			{
				this.items.Add(new ImagePair()
				{
					SmallImage = this.simages[i],
					LargeImage = this.limages[i],
				});
			}

			this.backgounds = new ObservableCollection<ImagePair>();
			for (int i = 0; i < 4; i++)
			{
				this.backgounds.Add(new ImagePair()
				{
					SmallImage = this.simages[8 + i],
					LargeImage = this.limages[8 + i],
				});
			}

			this.selectNextPerson = new DelegateCommand(new Action<object>(SelectNext), new Predicate<object>(CanSelectNext));
			this.selectPreviousPerson = new DelegateCommand(new Action<object>(SelectPrevious), new Predicate<object>(CanSelectPrevious));
			this.SelectedPair = this.Items[0];
		}

		public ObservableCollection<ImagePair> Items => this.items;
		public ObservableCollection<ImagePair> Backgounds => this.backgounds;
		public ImagePair SelectedPair
		{
			get => this.selectedPair;
			set
			{
				if (this.selectedPair != value)
				{
					this.selectedPair = value;
					this.OnPropertyChanged("SelectedPair");
					this.SelectNextPerson.InvalidateCanExecute();
					this.SelectPreviousPerson.InvalidateCanExecute();
				}
			}
		}
		public DelegateCommand SelectPreviousPerson => this.selectPreviousPerson;
		public DelegateCommand SelectNextPerson => this.selectNextPerson;

		private List<string> simages = new List<string>()
		{
			"/TileView;component/Images/TileView/DifferentSizes/Small/1.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/2.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/3.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/4.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/5.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/6.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/7.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/8.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/BG0.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/BG1.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/BG2.png",
			"/TileView;component/Images/TileView/DifferentSizes/Small/BG3.png",
		};

		private List<string> limages = new List<string>()
		{
			"/TileView;component/Images/TileView/DifferentSizes/Large/1.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/2.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/3.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/4.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/5.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/6.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/7.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/8.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/BG0.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/BG1.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/BG2.png",
			"/TileView;component/Images/TileView/DifferentSizes/Large/BG3.png",
		};

		private bool CanSelectNext(object target)
		{
			return this.items.IndexOf(this.SelectedPair) < this.items.Count - 1;
		}

		private void SelectNext(object target)
		{
			int selectedIndex = this.items.IndexOf(this.SelectedPair);
			this.SelectedPair = this.items[selectedIndex + 1];
		}

		private bool CanSelectPrevious(object target)
		{
			return this.items.IndexOf(this.SelectedPair) > 0;
		}

		private void SelectPrevious(object target)
		{
			int selectedIndex = this.items.IndexOf(this.SelectedPair);
			this.SelectedPair = this.items[selectedIndex - 1];
		}*/
	}
}

public class TileInfo
{
	/*public string Header { get; set; }
	public string Content { get; set; }*/

	public int DishCode { get; set; }
}
