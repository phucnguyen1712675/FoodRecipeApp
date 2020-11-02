using SharpDX.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FoodRecipeApp.Converter
{
    public class CountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int itemCount = (int)value;
            var result = new ObservableCollection<int>();
            for (int i = 1; i < itemCount; i++)
            {
                if (i % 3 == 0)
                {
                    result.Add(i);
                }
            }
            return result;
        }
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
            return value;
        }
	}
}
