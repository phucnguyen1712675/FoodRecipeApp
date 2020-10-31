using FoodRecipeApp.DTO;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using SharpDX.Collections;
using FoodRecipeApp.DAO;

namespace FoodRecipeApp.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddedStepWindow : Window
    {
        private List<Step> steps = new List<Step>();
        public AddedStepWindow(List<Step> steps)
        {
            InitializeComponent();
            this.steps = steps;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Step step in steps)
                 itemsControl.Items.Add(step);
/*            List<string> images = new List<string> { "C:\\Users\\LENOVO\\source\\repos\\phucnguyen1712675\\FoodRecipeApp\\FoodRecipeApp\\bin\\Debug\\Image\\5\\1\\2fcd0a86-522d-4044-87c8-be001c5521df.jpeg", "C:\\Users\\LENOVO\\source\\repos\\phucnguyen1712675\\FoodRecipeApp\\FoodRecipeApp\\bin\\Debug\\Image\\5\\1\\2fcd0a86-522d-4044-87c8-be001c5521df.jpeg" };
            Step step = new Step(1, "sthing ", images);*/

        }

    }
}
