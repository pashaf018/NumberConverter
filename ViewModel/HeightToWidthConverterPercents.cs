using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NumberConverter.ViewModel
{
	public class HeightToWidthConverterPercents : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double height)
			{
				double offset = 0;
				if (parameter != null)
				{
					double.TryParse(parameter.ToString(),NumberStyles.Any,CultureInfo.InvariantCulture, out offset);

					if (offset < 0)
					{
						return value;
					}

					double onePercent = height / 100;
					return onePercent * offset;
				}
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
