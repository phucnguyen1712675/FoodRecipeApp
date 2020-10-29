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
using Telerik.Windows.Controls.TimeBar;
using MahApps.Metro.Controls;

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
            /*DateItem di = new DateItem();
            di.DateString = "28.04.2014";
            itemsControl.Items.Add(di);
            itemsControl.Items.Add(di);
            itemsControl.Items.Add(di);
            string description = "aaaahkjahsdkjfhkjakjdhkjahsfhkjahdkjfhjakhdskjf kahdkjhakjhfd\n khakjhakjshdfkjhakj \n kjhakjshd";
            List<string> images = new List<string> {
                "1/2/aaa",
                "1/3/bbb"
                };
             Step steps = (new Step(1, description, images));
            itemsControl.Items.Add(steps);*/
            foreach (Step step in steps)
                itemsControl.Items.Add(step);
        }
    }
}
