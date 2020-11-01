using System;
using System.Globalization;
using System.Windows.Data;

namespace FoodRecipeApp.Converter
{
    public class TypesStringAddSpaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = (string)value;
            result = result.Replace(",", ", ");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
