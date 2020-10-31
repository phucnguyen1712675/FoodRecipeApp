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

        public static List<string> getAllImagesInStep(int dish, int stepNumber)
        {
            List<string> allImages = new List<string>();

            DataTable dataImage = ImageDAO.Instance.getImagesStepDish(dish.ToString(), stepNumber.ToString());
            foreach( DataRow row in dataImage.Rows)
            {
                allImages.Add(getFilePath(row));
            }
            return allImages;
        }

        public static string trimFilePathImage(string filePath)
        {
            return filePath.Remove(0, AppDomain.CurrentDomain.BaseDirectory.Length);
        }

        internal static void AddNewStepsToData(List<string> listImage, string stepNumber, int dishCode)
        {
            foreach(string image in listImage)
            {
                ImageDAO.Instance.addNewImage(dishCode, stepNumber, image);
            }
        }

        public static List<string> trimListFilePathImage(List<string> filePath)
        {
            for(int i=0; i< filePath.Count; i++)
            {
                filePath[i] = trimFilePathImage(filePath[i]);
            }
            return filePath;
        }
    }
}
