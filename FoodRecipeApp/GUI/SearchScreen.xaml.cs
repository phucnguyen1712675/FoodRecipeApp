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
		public ObservableCollection<Dish> FilterList;
		public SearchScreen()
		{
			InitializeComponent();

			//this.DataContext = Dish.GetDishes();
			AddAllCheckBox();


		}
		public int AddAllCheckBox()
        {
			List<CheckBox> ListABC = new List<CheckBox>();
			ListABC.Add(checkBox1);
			ListABC.Add(checkBox2);
			ListABC.Add(checkBox3);
			ListABC.Add(checkBox4);
			ListABC.Add(checkBox5);
			ListABC.Add(checkBox6);
			ListABC.Add(checkBox7);
			ListABC.Add(checkBox8);
			ListABC.Add(checkBox9);
			//ListABC.Add(checkBox10);
			ListABC.Add(checkBox11);
			ListABC.Add(checkBox12);
			ListABC.Add(checkBox13);
			ListABC.Add(checkBox14);
			ListABC.Add(checkBox15);
			ListABC.Add(checkBox16);
			ListABC.Add(checkBox17);
			ListABC.Add(checkBox18);
			ListCheckBoxes = new CheckedList(ListABC);
			return 0;
        }

		private void Check_Click(object sender, RoutedEventArgs e)
		{
			string FilterQuery = ListCheckBoxes.GetFilterQuery();
			FilterList = DishesDataSource.DishesFilterCollection(FilterQuery);
			this.DataContext =  FilterList;
			//MessageBox.Show(FilterQuery);
		}
		private void Search_Click(object sender, RoutedEventArgs e)
        {
			checkBox1.Content = "123";
		}

        private void Previous_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

		void Example_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//ApplicationThemeManager.GetInstance().ThemeChanged += this.Example_ThemeChanged;
		}

		private void Example_Unloaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//ApplicationThemeManager.GetInstance().ThemeChanged -= this.Example_ThemeChanged;
		}

		private void Example_ThemeChanged(object sender, System.EventArgs e)
		{
			this.Resources.MergedDictionaries.Clear();
			this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/TileView;component//Common/CommonTemplates.xaml", UriKind.RelativeOrAbsolute) });
			this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/TileView;component/Features/AutomaticScrolling/Resources.xaml", UriKind.RelativeOrAbsolute) });
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//this.DialogBox.Visibility = System.Windows.Visibility.Collapsed;
		}

        
    }

}
