﻿using FoodRecipeApp.DTO;
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
				default:
					e.Tile.Group.DisplayIndex = 0;
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
			_list = RecipeDAO.GetAll();
			DataListView.ItemsSource = _list;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("yeah");
		}
	}
}