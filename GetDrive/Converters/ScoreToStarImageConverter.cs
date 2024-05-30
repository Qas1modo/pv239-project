using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Converters
{
    public class ScoreToStarImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int score && int.TryParse(parameter.ToString(), out int starPosition))
            {
                var result = starPosition <= score ? "star_filled_primary_color.svg" : "star_filled_grey.svg";
                return result;
            }
            return "star_filled_grey.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
