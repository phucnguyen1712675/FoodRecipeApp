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
using System.Windows.Shapes;

namespace FoodRecipeApp.GUI
{
    /// <summary>
    /// Interaction logic for youtubeWindow.xaml
    /// </summary>
    public partial class youtubeWindow : Window
    {
        private string youtubeLink;

        public youtubeWindow(string youtubeLink)
        {
            InitializeComponent();
            this.youtubeLink = youtubeLink;
        }

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            youtubeLink = Dish.Display(youtubeLink, DishMedia.Width-20, DishMedia.Height-20);
            DishMedia.NavigateToString(youtubeLink);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DishMedia.Dispose();
        }
    }
}
