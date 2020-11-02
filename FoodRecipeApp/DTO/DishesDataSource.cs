using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    public class DishesDataSource: INotifyPropertyChanged
    {
		#pragma warning disable 67
		public event PropertyChangedEventHandler PropertyChanged;
		#pragma warning restore 67

        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        private static DishesDataSource _instance = null;
		public static DishesDataSource Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DishesDataSource();
					//var listener = OcPropertyChangedListener.Create();
					//listener.PropertyChanged += (sender, args) => { //do you stuff}
				}
				return _instance;
            }
        }

		private DishesCollection _allRecipesCollection;
		public DishesCollection AllRecipesCollection 
		{
			get => this._allRecipesCollection;
			set
			{
				if (this._allRecipesCollection != value)
				{
					this._allRecipesCollection = value;
					//RaisePropertyChanged();
					//this.OnPropertyChanged("Recipes");
				}
			}
		}

		private DishesCollection _favouriteDishesCollection;
        public DishesCollection FavouriteDishesCollection
        {
			get => this._favouriteDishesCollection;
			set
			{
				if (this._favouriteDishesCollection != value)
				{
					this._favouriteDishesCollection = value;
				}
			}
		}

        private DishesDataSource()
        {
            this._allRecipesCollection = DishesCollection.GetAllDishes();
            this._favouriteDishesCollection = DishesCollection.GetFavouriteDishes();
        }

		public static DishesCollection DishesFilterCollection(string queryFilter)
        {
            DishesCollection FilterDishes = DishesCollection.GetFilterDishes(queryFilter);
            return FilterDishes;
        }

		public static void getAllRecipeCollectAgain()
		{
			
		}
    }
}
