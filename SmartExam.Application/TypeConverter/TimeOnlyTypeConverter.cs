using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.TypeConverter
{
    public class TimeOnlyTypeConverter : ITypeConverter<string, TimeOnly>
    {
        public TimeOnly Convert(string source, TimeOnly destination, ResolutionContext context)
        {
            // Assuming the string is in the format "HH:mm"
            if (TimeOnly.TryParseExact(source, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            {
                return time;
            }
            throw new ArgumentException("Invalid time format. Expected format is HH:mm.");
        }
    }
}
