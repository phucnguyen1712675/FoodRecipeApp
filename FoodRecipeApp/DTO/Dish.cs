using FoodRecipeApp.DAO;
using FoodRecipeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    public class Dish : INotifyPropertyChanged
    {
        public int DishCode { get; set; }
        public bool IsLove { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string Video { get; set; }
        public string Loai { get; set; }
        public string ImagePath { get; set; }
        public List<Step> Steps { get; set; }
        public StepCollection StepsCollection { get; set; }

        public Dish(DataRow row)
        {
            DishCode = (int)row["Dish"];
            //IsLove = ((int)row["Love"] )== 1 ? true : false;
            Desciption = row["Description"].ToString();
            IsLove = (bool)row["Love"];
            Name = row["Name"].ToString();
            Video = row["Video"].ToString();
            Loai = row["Loai"].ToString();
            ImagePath = Images.getFilePath(row);
            Steps = Step.getAllStepsInDish(DishCode);

            StepsCollection = StepDataSource.GetStepsCollection(DishCode);
        }

        public Dish(bool isLove, string name, string imagePath, string description, string video, List<Step> steps, string loai)
        {
            IsLove = isLove;
            ImagePath = (imagePath);
            Desciption = description;
            Video = video;
            Steps = steps;
            Loai = loai;
            Name = name;
            DishCode = 0;

            StepsCollection = StepDataSource.GetStepsCollection(DishCode);
        }

        #pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore 67


        public static bool AddNewDishToData (bool isLove,string name,  string imagePath ,string description, string video, List<Step> steps, string loai)
        {
            if (imagePath == "" || description == "" || video == "" || steps == null || loai == "") return false;

            Dish newDish = new Dish(isLove,  name,  imagePath,  description,  video, steps,  loai);
            newDish.ImagePath = Images.trimFilePathImage(newDish.ImagePath);
            DishDAO.Instance.addNewDish(newDish);
            return true;
        }
        public static Regex YoutubeURIRegex = new Regex(@"[\?&]v=(?<v>[^&]+)");

        public static string simpleURL (string url)
        {
            Match m = YoutubeURIRegex.Match(url);
            string id = m.Groups["v"].Value;
            string newUrl = "http://www.youtube.com/embed/" + id;
            return newUrl;
        }

        public static int updateIsLoveDish(int DishCode)
        {
            return DishDAO.Instance.updateFavouriteRecipe(DishCode.ToString());
        }

        public static string Display(string url, double width, double height)
        {
            string newUrl = simpleURL(url);
            var page =
                "<html>" +
                "<head><meta http-equiv='X-UA-Compatible' content='IE=11'/>" +
                "<body>" + "\r\n" +
                "<iframe src=\"" + newUrl + "\" width=\"" + width + "\" height=\"" + height + "\" frameborder=\"0\" allowfullscreen></iframe>" +
                "</body></html>";
            return page;
        }

        public static int getNewestDishCode()
		{
            DataTable data = DishDAO.Instance.getNewestDishCode();
            int dishCode = (int)(data.Rows[0]["Dish"]);
            return dishCode;
		}
    }
}
