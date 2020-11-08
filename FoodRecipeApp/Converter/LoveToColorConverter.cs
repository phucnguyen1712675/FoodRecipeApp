using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FoodRecipeApp.Converter
{
    public class LoveToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var continent = (value as Dish).IsLove;

            switch (continent)
            {
                case true:
                    return new SolidColorBrush(Colors.Red);
                case false:
                    return new SolidColorBrush(Colors.Black);
                default:
                    break;
            }

            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
