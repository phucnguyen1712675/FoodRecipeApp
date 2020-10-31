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

		private static DishesDataSource _instance = null;
        private DishesCollection _dishesCollection;
        private DishesCollection _favouriteDishesCollection;

		//public event NotifyCollectionChangedEventHandler CollectionChanged;

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
            this._favouriteDishesCollection = null;
		}

		public DishesCollection DishesCollection
        {
            get
            {
                if (this._dishesCollection == null)
                {
                    this._dishesCollection = DishesCollection.GetDishes();
                    this._dishesCollection.CollectionChanged += DishesCollection_CollectionChanged;
                }
                return this._dishesCollection;
            }
        }

        public DishesCollection FavouriteDishesCollection
        {
            get
            {
                if (this._favouriteDishesCollection == null)
                {
                    this._favouriteDishesCollection = DishesCollection.GetFavouriteDishes();
                    this._favouriteDishesCollection.CollectionChanged += FavouriteDishesCollection_CollectionChanged;
                }
                return this._favouriteDishesCollection;
            }
        }

        private void CollectionItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//To do
	    }

		private void DishesCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				foreach (object Meeting in e.NewItems)
				{
					(Meeting as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(CollectionItem_PropertyChanged);
				}
			}

			if (e.OldItems != null)
			{
				foreach (object Meeting in e.OldItems)
				{
					(Meeting as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(CollectionItem_PropertyChanged);
				}
			}
		}

		private void FavouriteDishesCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				foreach (object Meeting in e.NewItems)
				{
					(Meeting as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(CollectionItem_PropertyChanged);
				}
			}

			if (e.OldItems != null)
			{
				foreach (object Meeting in e.OldItems)
				{
					(Meeting as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(CollectionItem_PropertyChanged);
				}
			}
		}

		
        public static DishesCollection DishesFilterCollection(string queryFilter)
        {
            DishesCollection FilterDishes = DishesCollection.GetFilterDishes(queryFilter);
            return FilterDishes;
        }
    }
}
