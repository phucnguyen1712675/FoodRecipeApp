using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FoodRecipeApp.ViewModels
{
	public class RecipeViewModel
	{
        public RecipeViewModel() { }

        private ObservableCollection<Dish> _recipes;
		public ObservableCollection<Dish> Recipes
		{
			get
			{
				if (this._recipes == null)
				{
                    this._recipes = DishesDataSource.Instance.DishesCollection;
				}
				return this._recipes;
			}
		}

        public string QuoteToShow
        {
            get
			{
               return QuotesDataSource.Instance.GetRandomQuote();
            }
        }
    }
}
