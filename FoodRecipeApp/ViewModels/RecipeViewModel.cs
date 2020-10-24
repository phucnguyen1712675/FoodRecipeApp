using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.ViewModels
{
	public class RecipeViewModel
	{
		private ObservableCollection<Dish> _recipes;

		public ObservableCollection<Dish> Recipes
		{
			get
			{
				if (this._recipes != null)
				{
					this._recipes = Dish.GetDishes();
				}
				return this._recipes;
			}
		}
	}
}
