using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NumberConverter.ViewModel
{
	public class HeightToWidthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double height)
			{
				double offset = 0;
				if (parameter != null)
				{
					double.TryParse(parameter.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out offset);
				}

				return height + offset;
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
