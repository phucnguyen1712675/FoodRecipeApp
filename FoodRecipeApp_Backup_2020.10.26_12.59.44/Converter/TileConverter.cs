using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace FoodRecipeApp.Converter
{
	public class TileConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			/*var name = (string)value;
			if (name.Contains("Arsenal") || name.Contains("Liverpool"))
			{
				return TileType.Single;
			}*/
			return TileType.Double;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
