using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodRecipeApp.DTO
{
	public class DishesCollection : ObservableCollectionPropertyNotify<Dish>
    {
		public static DishesCollection GetAllDishes()
		{
			DishesCollection dishes = new DishesCollection();
			DataTable data = DishDAO.Instance.getAllDishes();

            foreach (var dish in from DataRow row in data.Rows
                                 let dish = new Dish(row)
                                 select dish)
            {
                dishes.Add(dish);
            }

            return dishes;
		}

		public static DishesCollection GetFilterDishes(string queryFilter)
        {
            DishesCollection dishes = new DishesCollection();
            DataTable data = DishDAO.Instance.getFilterDishes(queryFilter);

            foreach (var dish in from DataRow row in data.Rows
                                 let dish = new Dish(row)
                                 select dish)
            {
                dishes.Add(dish);
            }

            return dishes;
        }
        public static DishesCollection GetDishByName(string queryFilter)
        {
            DishesCollection dishes = new DishesCollection();
            DataTable data = DishDAO.Instance.getDishByName(queryFilter);

            foreach (var dish in from DataRow row in data.Rows
                                 let dish = new Dish(row)
                                 select dish)
            {
                dishes.Add(dish);
            }

            return dishes;
        }

        public void SetDefaultPosition()
        {
            var list = this.OrderBy(c => c.DishCode).ToList();
            this.Clear();
            foreach (var item in list)
            {
                this.Add(item);
            }
        }

        public void SetAscendingPositionAccordingToName()
        {
            var list = this.OrderBy(c => c.Name).ToList();
            this.Clear();
            foreach (var item in list)
            {
                this.Add(item);
            }
        }

        public void SetDescendingPositionAccordingToName()
        {
            var list = this.OrderByDescending(c => c.Name).ToList();
            this.Clear();
            foreach (var item in list)
            {
                this.Add(item);
            }
        }
    }
}