using System.ComponentModel;

namespace CircleApp.Converts;

public class EnumDescriptionConverter : Microsoft.UI.Xaml.Data.IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return string.Empty;

        var f = value.GetType().GetField(value.ToString());
        var attr = f.GetCustomAttributes(typeof(DescriptionAttribute), true)?.FirstOrDefault() as DescriptionAttribute;

        return attr?.Description ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return value;
    }
}
