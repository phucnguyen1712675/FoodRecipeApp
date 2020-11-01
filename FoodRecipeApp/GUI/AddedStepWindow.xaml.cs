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
using System.ComponentModel;
using System;

namespace FoodRecipeApp.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddedStepWindow : Window
    {
        public delegate void DeathHandler();
        public event DeathHandler Dying;

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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dying?.Invoke();
        }


    }
}
