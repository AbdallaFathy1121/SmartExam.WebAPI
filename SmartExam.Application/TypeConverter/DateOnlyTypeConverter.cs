using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.TypeConverter
{
    public class DateOnlyTypeConverter : ITypeConverter<string, DateOnly>
    {
        public DateOnly Convert(string source, DateOnly destination, ResolutionContext context)
        {
            // Assuming the string is in the format "yyyy-MM-dd"
            if (DateOnly.TryParseExact(source, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }
            throw new ArgumentException("Invalid date format. Expected format is yyyy-MM-dd.");
        }
    }
}
