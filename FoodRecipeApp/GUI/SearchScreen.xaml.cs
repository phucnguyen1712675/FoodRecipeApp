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

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for SearchScreen.xaml
	/// </summary>
	public partial class SearchScreen : Page
	{
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
			FullList = DishesDataSource.Instance.AllRecipesCollection;
			ShowAll();

			// Getting the currently selected ListBoxItem
			// Note that the ListBox must have
			// IsSynchronizedWithCurrentItem set to True for this to work
		}
		private childItem FindVisualChild<childItem>(DependencyObject obj)
	where childItem : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem)
				{
					return (childItem)child;
				}
				else
				{
					childItem childOfChild = FindVisualChild<childItem>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
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
			this.FilterList = DishesDataSource.DishesFilterCollection(FilterQuery);
			ShowSearchResult();
		}
		private void Search_Click(object sender, RoutedEventArgs e)
        {
		}

        private void Previous_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//this.DialogBox.Visibility = System.Windows.Visibility.Collapsed;
		}

		private void radTileView_TileStateChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{
			RadTileViewItem item = e.OriginalSource as RadTileViewItem;
			if (item != null)
			{
				RadFluidContentControl fluid = item.ChildrenOfType<RadFluidContentControl>().FirstOrDefault();
				if (fluid != null)
				{
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
			}
		}

        private void Unchecked_Click(object sender, RoutedEventArgs e)
        {
			ShowAll();
			ListCheckBoxes.UncheckAll();
		}
	}

}
