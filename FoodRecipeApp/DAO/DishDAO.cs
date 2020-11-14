using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodRecipeApp.DAO
{
    class DishDAO
    {
        private static DishDAO instance;

        public static DishDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new DishDAO();
                return instance;
            }
            private set => instance = value;
        }

        private DishDAO() { }

        public DataTable getAllDishes()
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getAllDishes");
        }

        public int updateFavouriteRecipe(string DishCode)
		{
            return DataProvider.Instance.ExecuteNonQuery("EXEC USP_updateFavouriteDishes @DishCode ", new object[] { DishCode });
		} 

        internal void addNewDish(Dish newDish)
        {
            string IsLove = (newDish.IsLove == true ? 1 : 0 ).ToString();
            string Name = newDish.Name;
            string Video = newDish.Video;
            string Description = newDish.Desciption;
            string FilePath = newDish.ImagePath;
            string Loai = newDish.Loai;
            
            DataTable data =  DataProvider.Instance.ExecuteQuery("EXEC USP_addNewDish @IsLove , @Name , @Video , @Description , @FilePath , @Loai ", new object[] { IsLove , Name, Video, Description, FilePath, Loai });
            
            int dishCode = (int)(data.Rows[0]["Dish"]);

            Step.AddNewStepsToData(newDish.Steps, dishCode);
        }
   
        public DataTable getNewestDishCode()
		{
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getNewestDish");
		}

        internal DataTable getUpdateDateByDishCode(string Dish)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getUpdateDateByDishCode @Dish ", new object[] { Dish });
        }
    }
}
