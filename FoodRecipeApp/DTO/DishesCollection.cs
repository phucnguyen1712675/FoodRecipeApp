﻿using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class DishesCollection: System.Collections.ObjectModel.ObservableCollection<Dish>
	{
        public static DishesCollection GetDishes()
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

        public static List<Dish> GetDishList()
        {
            List<Dish> dishes = new List<Dish>();
            DataTable data = DishDAO.Instance.getAllDishes();
            foreach (DataRow row in data.Rows)
            {
                Dish dish = new Dish(row);
                dishes.Add(dish);
            }
            return dishes;
        }
    }
}