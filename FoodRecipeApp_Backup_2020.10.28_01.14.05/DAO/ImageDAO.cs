using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DAO
{
    class ImageDAO
    {
        private static ImageDAO instance;

        public static ImageDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ImageDAO();
                return instance;
            }
            private set => instance = value;
        }

        private ImageDAO() { }

        public DataTable getImagesStepDish(string dish, string stepNumber)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getImageStepDish @Dish , @Step ", new object[] { dish, stepNumber });
        }

        public DataTable getImageDish(string dish)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getImageDish @Dish ", new object[] { dish });
        }
    }
}
