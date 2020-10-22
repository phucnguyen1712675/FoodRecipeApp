using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    public class Images
    {

        public static string getFilePath(DataRow row)
        {
            string result = AppDomain.CurrentDomain.BaseDirectory + row["FilePath"].ToString();
            return result;
        }

        public static string getFileDish(int dish)
        {
            DataRow row = ImageDAO.Instance.getImageDish(dish.ToString()).Rows[0];
            string result  = getFilePath(row);
            return result;
        }

        public static List<String> getAllImagesInStep(int dish, int stepNumber)
        {
            List<string> allImages = new List<string>();

            DataTable dataImage = ImageDAO.Instance.getImagesStepDish(dish.ToString(), stepNumber.ToString());
            foreach( DataRow row in dataImage.Rows)
            {
                allImages.Add(getFilePath(row));
            }
            return allImages;
        }
    }
}
