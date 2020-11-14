using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace FoodRecipeApp.DTO
{
    public class Dish : INotifyPropertyChanged, ICloneable
    {
        public int DishCode { get; set; }
        public bool IsLove { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string Video { get; set; }
        public string Loai { get; set; }
        public string ImagePath { get; set; }
        public string DateCreate { get; set; }
        public List<Step> Steps { get; set; }

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
            DateCreate = DateTime.ParseExact(row["RecordedDate"].ToString(),"M/d/yyyy h:mm:ss tt",System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
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
        }

        public static string getUpdateDateByDishCode(int dishCode)
        {
            DataRow row = DishDAO.Instance.getUpdateDateByDishCode(dishCode.ToString()).Rows[0];
            string DateUpdate = DateTime.ParseExact(row["RecordedDate"].ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

            return DateUpdate;
        }

#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67

        public static bool AddNewDishToData(bool isLove, string name, string imagePath, string description, string video, List<Step> steps, string loai)
        {
            if (imagePath == "" || description == "" || video == "" || steps == null || loai == "") return false;

            Dish newDish = new Dish(isLove, name, imagePath, description, video, steps, loai);
            newDish.ImagePath = Images.trimFilePathImage(newDish.ImagePath);
            DishDAO.Instance.addNewDish(newDish);
            return true;
        }
        public static Regex YoutubeURIRegex = new Regex(@"[\?&]v=(?<v>[^&]+)");

        public static string simpleURL(string url)
        {
            Match m = YoutubeURIRegex.Match(url);
            string id = m.Groups["v"].Value;
            string newUrl = "http://www.youtube.com/embed/" + id;
            return newUrl;
        }

        public static int updateIsLoveDish(int DishCode) => DishDAO.Instance.updateFavouriteRecipe(DishCode.ToString());

        public static string Display(string url, double width, double height)
        {
            string newUrl = simpleURL(url);
            var page =
                "<html>" +
                "<head><meta http-equiv='X-UA-Compatible' content='IE=11'/>" +
                "<body style = \"margin:0px;padding:0px;overflow:hidden\">" + "\r\n" +
                "<iframe src = \"" + newUrl +"\"" + 
                " frameborder = \"0\" style = \"overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;top:0px;left:0px;right:0px;bottom:0px\" height = \"100%\" width = \"100%\"></ iframe >" + "\r\n" +
                "</body ></html>";
            return page;
        }

        public static int getNewestDishCode()
        {
            DataTable data = DishDAO.Instance.getNewestDishCode();
            int dishCode = (int)(data.Rows[0]["Dish"]);
            return dishCode;
        }

        public static string CreateQueryLinQ(string str, string Name)
        {
            str = str.Replace("(", " ( ").Replace(")", " ) ");
            str = Dish.TrimSpacesBetweenString(str).ToLower();
            str = str.Replace("or (", "||(").Replace("and (", "&&(").Replace(") and", ")&&").Replace(") or", ")||").Replace(") )", "))").Replace("( (", "((").Replace(" and ", " && ").Replace(" or ", " || ");

            if (str[0] != '(') str = Name + ".Contains(\"" + str;

            //(_
            str = str.Replace("( ", "(" + Name + ".Contains(\"");

            //_)
            str = str.Replace(" )", "\") )");

            // (_or_) (_and_)
            str = str.Replace(" || ", "\")" + " || " + Name + ".Contains(\"");
            str = str.Replace(" && ", "\")" + " && " + Name + ".Contains(\"");

            //_and( _or(
            str = str.Replace(" &&(", "\") &&(");
            str = str.Replace(" ||(", "\") ||(");

            // )and_  )or_ 
            str = str.Replace(")&& ", ")&& " + Name + ".Contains(\"");
            str = str.Replace(")|| ", ")|| " + Name + ".Contains(\"");

            if (str[str.Length - 1] != ')') str = str + "\")";
            return str;
        }

        public static string RemoveDiacritics(string s)
        {

            string normalizedString = null;
            StringBuilder stringBuilder = new StringBuilder();
            normalizedString = s.ToLower().Normalize(NormalizationForm.FormD);
            int i = 0;
            char c = '\0';

            for (i = 0; i <= normalizedString.Length - 1; i++)
            {
                c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            stringBuilder = stringBuilder.Replace("đ", "d");
            return stringBuilder.ToString();
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

        public static bool checkQuery(string queryStr)
        {
            int result = 0;
            for (int i = 0; i < queryStr.Length; i++)
            {
                if (queryStr[i] == '\"') result++;
            }
            if (result % 2 == 0)
            {
                result = 0;
                for (int i = 0; i < queryStr.Length; i++)
                {
                    if (queryStr[i] == '(') result++;
                    if (queryStr[i] == ')') result--;
                }
                return result == 0 ? true : false;

            }
            else return false;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
