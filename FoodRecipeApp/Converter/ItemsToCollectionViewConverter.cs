using FoodRecipeApp.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FoodRecipeApp.Converter
{
    public class ItemsToCollectionViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                CollectionViewSource GroupedRecipes = new CollectionViewSource();
                GroupedRecipes.GroupDescriptions.Add(new PropertyGroupDescription("IsLove"));
                var recipes = (value as IEnumerable).Cast<Dish>().ToList();
                GroupedRecipes.Source = recipes;

                return GroupedRecipes.View;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
