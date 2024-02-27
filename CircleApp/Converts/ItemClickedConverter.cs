namespace CircleApp.Converts;

public class ItemClickedConverter : Microsoft.UI.Xaml.Data.IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var args = value as ItemClickEventArgs;

        if (args != null)
            return args.ClickedItem;

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter,
        string language)
    {
        throw new NotImplementedException();
    }
}
