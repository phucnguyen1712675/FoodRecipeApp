using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using FoodRecipeApp.DTO;
using FoodRecipeApp.ViewModels;
using Telerik.Windows.Controls;


namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
			this.Loaded += Example_Loaded;
			this.Unloaded += Example_Unloaded;
		}

		private void Example_Loaded(object sender, RoutedEventArgs e)
		{
			//ApplicationThemeManager.GetInstance().ThemeChanged += this.Example_ThemeChanged;
		}

		private void Example_Unloaded(object sender, RoutedEventArgs e)
		{
			//ApplicationThemeManager.GetInstance().ThemeChanged -= this.Example_ThemeChanged;
		}

		private void Example_ThemeChanged(object sender, EventArgs e)
		{
			this.Resources.MergedDictionaries.Clear();
			this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/TileList;component/BindingItemsSource/WPF/Resources.xaml", UriKind.RelativeOrAbsolute) });
		}

		private void OnAutoGeneratingTile(object sender, AutoGeneratingTileEventArgs e)
		{
			var employeePosition = (e.Tile.DataContext as Dish).Loai;

			switch (employeePosition)
			{
				case "Sales Representative":
					e.Tile.Group.DisplayIndex = 0;
					break;
				case "Sales Manager":
					e.Tile.Group.DisplayIndex = 1;
					break;
				case "Vice President, Sales":
					e.Tile.Group.DisplayIndex = 2;
					break;

			}
		}
		/*private NasdaqViewModel _viewModel;
		public MainPage()
		{
			InitializeComponent();
			this._viewModel = this.LayoutRoot.Resources["NasdaqViewModel"] as NasdaqViewModel;

			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(5);
			timer.Tick += OnTimerTick;
			timer.Start();
		}

		void OnTimerTick(object sender, EventArgs e)
		{
			_viewModel.UpdateDisplayValue();
		}

		class Recipe
		{
			public string Tittle { get; set; }
			public string ImageSource { get; set; }
		}

		class RecipeDAO
		{
			public static BindingList<Recipe> GetAll()
			{
				var result = new BindingList<Recipe>()
				{
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"},
					new Recipe() { Tittle="Gà kho xả ớt", ImageSource="/Resources/abubu.jpeg"}
				};

				return result;
			}
		}

		BindingList<Recipe> _list = new BindingList<Recipe>();

		private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			//ImageBrush.ImageSource = ;
			_list = RecipeDAO.GetAll();
			//DataListView.ItemsSource = _list;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("yeah");
		}*/
	}
}
