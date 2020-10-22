using FoodRecipeApp.DTO;
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

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for DetailScreen.xaml
	/// </summary>
	public partial class DetailScreen : Page
	{
		public DetailScreen()
		{
			InitializeComponent();
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
/*            List<Dish> dishes = Dish.getAllDish();
            Dish temp = dishes[3];
            tempLabel.Content = temp.Name;
            Step tempStep = temp.Steps[1];
            var bitmap = new BitmapImage(
                new Uri(tempStep.ListImage[0], UriKind.Absolute)
            );
            tempImage.Source = bitmap;*/
        }
    }
}
