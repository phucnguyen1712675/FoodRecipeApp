﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp
{
    public class ObservableCollectionPropertyNotify<T> : ObservableCollection<T>
    {
        //OnCollectionChange method is protected, accesible only within a child class in this case. This is why  
        //I made a new Collection class with a public method Refresh.  
        public void Refresh()
        {
            for (var i = 0; i < this.Count(); i++)
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }
}
