using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for AddRecipe.xaml
	/// </summary>
	public partial class AddRecipe : Page
	{
		public AddRecipe()
		{
			InitializeComponent();
		}

        #region clickMethod
        private void AddStepImagesButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
        "All files (*.*)|*.*";

            fileDialog.Multiselect = true;
            fileDialog.Title = "My Image Browser";

            DialogResult dr = fileDialog.ShowDialog();

            if(dr == DialogResult.OK)
            {
                List<string> result = new List<string>();
                foreach(string file in fileDialog.FileNames)
                {
                    try
                    {
                        result.Add(file);
                    }
                    catch(SecurityException ex)
                    {
                        System.Windows.MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                "Error message: " + ex.Message + "\n\n" +
                                "Details (send to Support):\n\n" + ex.StackTrace
                                );
                    }
                    catch(Exception ex)
                    {
                        System.Windows.MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
                ImageListView.ItemsSource = result;
            }
        }

        #endregion

        #region textHint_textChange effects
        private void DishMediaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DishMediaTextBox.Text.Length == 0)
            {
                HintDishMediaTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                HintDishMediaTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void DescriptionDishRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string DesciptionDishString = new TextRange(DescriptionDishRichTextBox.Document.ContentStart, DescriptionDishRichTextBox.Document.ContentEnd).Text;
            if (DesciptionDishString.Length - 2 == 0)
            {
                HintDishDescriptionTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                HintDishDescriptionTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void DescriptionStepRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string DesciptionStepString = new TextRange(DescriptionStepRichTextBox.Document.ContentStart, DescriptionStepRichTextBox.Document.ContentEnd).Text;
            if (DesciptionStepString.Length - 2 == 0)
            {
                HintDesciptionStepDishTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                HintDesciptionStepDishTextBlock.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        
    }
}
