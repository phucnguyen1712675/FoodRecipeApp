using FoodRecipeApp.DAO;
using FoodRecipeApp.ViewModels;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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

        //SEARCH TEXTBOX + FILTER
        public static string CreateQuery(string str, string Name)
        {
            if (str[0] != '(') str = Name + " like N'%" + str;

            //(_
            str = str.Replace("( ", "(" + Name + " like N'%");

            //_)
            str = str.Replace(" )", "%' )");

            // (_or_) (_and_)
            str = str.Replace(" or ", "%'" + " or " + Name + " like N'%");
            str = str.Replace(" and ", "%'" + " and " + Name + " like N'%");

            //_and( _or(
            str = str.Replace(" and(", "%' and(");
            str = str.Replace(" or(", "% ' or(");

            return str;
        }

        public static string TrimSpacesBetweenString(string s)
        {
            var mystring = s.Split(new string[] { " " }, StringSplitOptions.None);
            string result = string.Empty;
            foreach (var mstr in mystring)
            {
                var ss = mstr.Trim();
                if (!string.IsNullOrEmpty(ss))
                {
                    result = result + ss + " ";
                }
            }
            return result.Trim();

        }

        public static List<Dish> AdvanceSearch(string strTextBox , string strFilter)
        {
            //run khi event textchange || mousedown in checkbox groub
            string result1 = null;
            string result2 = null;
            string result = null;
            if (strTextBox != "") {
                strTextBox = strTextBox.Replace("(", " ( ").Replace(")", " ) ");
                strTextBox = Dish.TrimSpacesBetweenString(strTextBox).ToLower();

                strTextBox = strTextBox.Replace("or (", "or(").Replace("and (", "and(").Replace(") and", ")and").Replace(") or", ")or").Replace(") )", "))").Replace("( (", "((");

                string str1 = Dish.CreateQuery(strTextBox, "Name");
                string str2 = Dish.CreateQuery(strTextBox, "dbo.ufn_removeMark(Name)");

                result1 = "select * from DISH where ( " + str1 + ") OR (" + str2 + ")";
                result = result1;
            }

            if(strFilter != "")
            {
                result2 = "select * from DISH where not exists((select Item from dbo.SplitInts(N'" + strFilter + "',',')) except(select Item from dbo.SplitInts(Loai, ',')))";
                if (result != null) result = result + " intersect";
                result = result + result2;
            }

            List<Dish> resultDishes = new List<Dish>();

            DataTable resultTable = DishDAO.Instance.AdvanceSearch(result);
            foreach(DataRow row in resultTable.Rows)
            {
                resultDishes.Add(new Dish(row));
            }
            return resultDishes;
        }

    }
}
