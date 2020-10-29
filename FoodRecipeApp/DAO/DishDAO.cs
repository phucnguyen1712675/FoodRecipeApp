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
        public DataTable getFilterDishes(string filterQuery)
        {
            
            //string test = "EXEC USP_getDishByTypes @List = N'" + filterQuery + "'";
            //MessageBox.Show(test);
            //return DataProvider.Instance.ExecuteQuery("EXEC USP_getDishByTypes @List = N'Mặn,chay'");
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getDishByTypes @List = N'" + filterQuery + "'");
        }
    }
}
