using SharpDX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FoodRecipeApp.Converter
{
	public class IsLoveImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool isLove = (bool)value;
			return isLove;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//int dish = (int)parameter;

			bool isLove = (bool)value;
			return isLove;
		}
	}
}
