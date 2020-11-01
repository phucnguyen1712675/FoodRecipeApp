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
    /// Interaction logic for MyWebBrowser.xaml
    /// </summary>
    public partial class HtmlBox : UserControl
    {
        public HtmlBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HtmlTextProperty = DependencyProperty.Register("HtmlText", typeof(string), typeof(HtmlBox));

        public string HtmlText
        {
            get { return (string)GetValue(HtmlTextProperty); }
            set { SetValue(HtmlTextProperty, value); }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == HtmlTextProperty)
            {
                DoBrowse();
            }
        }
        private void DoBrowse()
        {
            if (!string.IsNullOrEmpty(HtmlText))
            {
                this.browser.NavigateToString(HtmlText);
            }
        }
    }
}

