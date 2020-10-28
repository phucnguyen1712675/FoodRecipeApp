using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class DishesDataSource
	{
        private static DishesDataSource _instance = null;
		private DishesCollection _dishesCollection;

		public static DishesDataSource Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DishesDataSource();
                }
                return _instance;
            }
        }

        private DishesDataSource() 
		{
			this._dishesCollection = null;
		}

       

		public DishesCollection DishesCollection 
		{ 
			get
			{
				if (this._dishesCollection == null)
				{
					this._dishesCollection = DishesCollection.GetDishes();
				}
				return this._dishesCollection;
			}
		}
	}
}
