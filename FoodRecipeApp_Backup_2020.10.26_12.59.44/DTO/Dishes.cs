using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class Dishes
	{
        public IEnumerable<Dish> GetData(int maxItems)
        {
            List<Dish> dishList = Dish.GetDishList();
            int n = dishList.Count;
            //List<Dish> randomlyDishList = new List<Dish>();
            IEnumerable<Dish> randomlyDishList = Enumerable.Range(1, maxItems).Select(x => dishList[MyRandom.Instance.Next(n)]);
            return randomlyDishList;
        }

        /// <summary>
        /// Generates the random business object.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <returns></returns>
        public Dish GenerateRandomBusinessObject()
        {
            List<Dish> dishList = Dish.GetDishList();
            int n = dishList.Count;
            //List<Dish> randomlyDishList = new List<Dish>();
            Dish randomlyDish = dishList[MyRandom.Instance.Next(n)];
            return randomlyDish;
        }
    }
}
