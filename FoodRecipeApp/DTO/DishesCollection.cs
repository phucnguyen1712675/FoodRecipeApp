using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace FoodRecipeApp.DTO
{
    public class DishesCollection : ObservableCollectionPropertyNotify<Dish>
    {
        private Dictionary<string, Delegate> sortMethod = new Dictionary<string, Delegate> {
            { "Default", new Func<DishesCollection,List<Dish>>(SetDefaultPosition)},
            { "Ascending Ordered By Name", new Func<DishesCollection,List<Dish>>(SetAscendingPositionAccordingToName)},
            { "Descending Ordered By Name", new Func<DishesCollection,List<Dish>>(SetDescendingPositionAccordingToName)},
            { "Ascending Ordered By Date", new Func<DishesCollection,List<Dish>>(SetAscendingPositionAccordingToDate)},
            { "Descending Ordered By Date", new Func<DishesCollection,List<Dish>>(SetDescendingPositionAccordingToDate)}
        };

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

        public static DishesCollection cloneFromListDishes(List<Dish> dises)
        {
            DishesCollection collectionDishes = new DishesCollection();
            foreach (var dish in dises) collectionDishes.Add(dish);
            return collectionDishes;
        }

        public void SetSort (string method)
        {
            if (sortMethod.ContainsKey(method))
            {
                List<Dish> list = (List<Dish>)sortMethod[method].DynamicInvoke(this);
                this.Clear();
                foreach (var item in list)
                {
                    this.Add(item);
                }
            }
        }

        public static List<Dish> SetDefaultPosition(DishesCollection dishes)
        {
            return dishes.OrderBy(c => c.DishCode).ToList();
        }

        public static List<Dish> SetAscendingPositionAccordingToName(DishesCollection dishes)
        {
            return dishes.OrderBy(c => c.Name).ToList();
        }

        public static List<Dish> SetDescendingPositionAccordingToName(DishesCollection dishes)
        {
            return dishes.OrderByDescending(c => c.Name).ToList();
        }

        public static List<Dish> SetAscendingPositionAccordingToDate(DishesCollection dishes)
        {
            return dishes.OrderBy(c => Convert.ToDateTime(c.DateCreate)).ToList();
        }

        public static List<Dish> SetDescendingPositionAccordingToDate(DishesCollection dishes)
        {
            return dishes.OrderByDescending(c => Convert.ToDateTime(c.DateCreate)).ToList();
        }

        public void Filtering(string ThingToFilter)
        {
            var clonedList = this.Select(objEntity => (Dish)objEntity.Clone()).ToList();
            ObservableCollection<Dish> clonedCollection = new ObservableCollection<Dish>(clonedList);

            this.Clear();

            var filterList = from item in clonedCollection
                             let loai = item.Loai.ToLower()
                             where loai.Contains(ThingToFilter.ToLower())
                             select item;

            foreach (var item in filterList)
            {
                this.Add(item);
            }

        }
    }
}