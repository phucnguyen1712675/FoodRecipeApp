using FoodRecipeApp.DTO;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataVisualization.Map.BingRest;
using CheckBox = System.Windows.Controls.CheckBox;
using Control = System.Windows.Controls.Control;
using Image = System.Windows.Controls.Image;
using ListView = System.Windows.Controls.ListView;
using MessageBox = System.Windows.Forms.MessageBox;
using RichTextBox = System.Windows.Controls.RichTextBox;
using TextBox = System.Windows.Controls.TextBox;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace FoodRecipeApp.GUI
{
	/// <summary>
	/// Interaction logic for AddRecipe.xaml
	/// </summary>
	public partial class AddRecipe : Page
	{
        public List<Step> steps = new List<Step>();

		public AddRecipe()
		{
			InitializeComponent();
		}

        public string AddAllCheckBoxes()
        {
            List<CheckBox> ListCheckBox = new List<CheckBox>();
            ListCheckBox.Add(checkBox1);
            ListCheckBox.Add(checkBox2);
            ListCheckBox.Add(checkBox3);
            ListCheckBox.Add(checkBox4);
            ListCheckBox.Add(checkBox5);
            ListCheckBox.Add(checkBox6);
            ListCheckBox.Add(checkBox7);
            ListCheckBox.Add(checkBox8);
            ListCheckBox.Add(checkBox9);
            ListCheckBox.Add(checkBox10);
            ListCheckBox.Add(checkBox11);
            ListCheckBox.Add(checkBox12);
            ListCheckBox.Add(checkBox13);
            ListCheckBox.Add(checkBox14);
            ListCheckBox.Add(checkBox15);
            ListCheckBox.Add(checkBox16);
            ListCheckBox.Add(checkBox17);

            string loai = null;
            foreach (CheckBox checkBox in ListCheckBox)
            {
                if (checkBox.IsChecked == true)
                    if (loai == null)
                    {
                        loai = loai + checkBox.Content;
                    }
                    else
                    {
                        loai = loai + "," + checkBox.Content;
                    }
            }
            return loai;
        }

        #region clickMethod

        private void AddDishImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                                 "All files (*.*)|*.*";
            fileDialog.Title = "My Image Browser";
            DialogResult dr = fileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string imagePath = fileDialog.FileName;
                var folder = AppDomain.CurrentDomain.BaseDirectory;

                var bitmap = new BitmapImage(
                    new Uri(imagePath, UriKind.Absolute)
                );
                DishImage.Source = bitmap;
                DishImage.Tag = imagePath;
                string DishPathImage = DishImage.Tag.ToString();
            }

        }

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
                if (result != null)
                {
                    ImageListView.ItemsSource = result;
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn ảnh");
                }

            }
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            string Description = new TextRange(DescriptionStepRichTextBox.Document.ContentStart, DescriptionStepRichTextBox.Document.ContentEnd).Text;
            List<string> ListImage = (List<string>)ImageListView.ItemsSource;
            int StepNumber;
            if (steps.Count == 0)
            {
                StepNumber = 1;
            }
            else StepNumber = steps.Count + 1;

            Step newStep = Step.CreateStep(StepNumber, Description, ListImage);
            if (newStep == null)
            {
                System.Windows.MessageBox.Show("Thêm thất bại\nTrường dữ liệu trống !!");
            }
            else
            {
                steps.Add(newStep);
                System.Windows.MessageBox.Show("Thêm thành công");
                DescriptionStepRichTextBox.Document.Blocks.Clear();
                ImageListView.ItemsSource = null;
                StepNumberLabel.Content = "Bước " + (steps.Count() + 1).ToString();
            }
        }

        private void FinishStep_Click(object sender, RoutedEventArgs e)
        {
            if (steps.Count == 0)
            {
                MessageBox.Show("Bạn chưa thêm bước làm nào cả");
            }
            else
            {
                var addedStepsScreen = new AddedStepWindow(steps);
                addedStepsScreen.Show();
            }
        }

        private void AddMediaButton_Click(object sender, RoutedEventArgs e)
        {
            string linkMedia = DishMediaTextBox.Text;
            linkMedia = Dish.Display(linkMedia, DishMedia.Width - 10, DishMedia.Height - 10);
            DishMedia.NavigateToString(linkMedia);
        }

        private void CancelDishButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void AddDishButton_Click(object sender, RoutedEventArgs e)
        {
            string dishName = DishNameTextBox.Text;
            string descriptionDish = new TextRange(DescriptionDishRichTextBox.Document.ContentStart, DescriptionDishRichTextBox.Document.ContentEnd).Text;
            string video = DishMediaTextBox.Text;
            string imagePath = DishImage.Tag.ToString();
            string loai = AddAllCheckBoxes();

            bool isLove = (bool)IsLoveDishCheckBox.IsChecked;
            if (Dish.AddNewDishToData(isLove, dishName, imagePath, descriptionDish, video, steps, loai))
            {
                MessageBox.Show("Thêm thành công vào database");
                ClearAll();

            }
            else
            {
                MessageBox.Show("dữ liệu thiếu hoặc sai !");
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

        private void DishNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DishNameTextBox.Text.Length == 0)
            {
                HintDishNameTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                HintDishNameTextBlock.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region other_stuff
        private void ClearAll()
        {
            steps.Clear();
            //TODO refresh this page
           
        }
        #endregion

    }
}

