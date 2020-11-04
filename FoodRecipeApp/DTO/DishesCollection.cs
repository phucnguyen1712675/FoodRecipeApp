using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodRecipeApp.DTO
{
	public class DishesCollection : ObservableCollection<Dish>
	{

		public static DishesCollection GetAllDishes()
		{
			DishesCollection dishes = new DishesCollection();
			DataTable data = DishDAO.Instance.getAllDishes();
			foreach (DataRow row in data.Rows)
			{
				Dish dish = new Dish(row);
				dishes.Add(dish);
			}
			return dishes;
		}

		/*public static BindingList<Dish> GetAllDishes()
		{
			BindingList<Dish> dishes = new BindingList<Dish>();
			DataTable data = DishDAO.Instance.getAllDishes();
			foreach (DataRow row in data.Rows)
			{
				Dish dish = new Dish(row);
				dishes.Add(dish);
			}
			return dishes;
		}*/

		public static DishesCollection GetFilterDishes(string queryFilter)
        {
            DishesCollection dishes = new DishesCollection();
            DataTable data = DishDAO.Instance.getFilterDishes(queryFilter);
            //MessageBox.Show(data.Rows.Count.ToString());
            foreach (DataRow row in data.Rows)
            {
                Dish dish = new Dish(row);
                dishes.Add(dish);
            }
            return dishes;
        }
        public static DishesCollection GetDishByName(string queryFilter)
        {
            DishesCollection dishes = new DishesCollection();
            DataTable data = DishDAO.Instance.getDishByName(queryFilter);
            //MessageBox.Show(data.Rows.Count.ToString());
            foreach (DataRow row in data.Rows)
            {
                Dish dish = new Dish(row);
                dishes.Add(dish);
            }
            return dishes;
        }
        public static DishesCollection GetFavouriteDishes()
		{
            DishesCollection dishes = new DishesCollection();
            DataTable data = DishDAO.Instance.getFavouriteDishes();
            foreach (DataRow row in data.Rows)
            {
                Dish dish = new Dish(row);
                dishes.Add(dish);
            }
            return dishes;
        }
	}
}