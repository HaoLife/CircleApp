using Microsoft.UI.Xaml.Markup;

namespace CircleApp.Markup;

[MarkupExtensionReturnType(ReturnType = typeof(int?))]
public class NullableIntExtension : MarkupExtension
{
    public int Value { get; set; }

    public bool IsNull { get; set; }

    protected override object? ProvideValue()
    {
        if (IsNull)
        {
            return null;
        }

        return Value;
    }
}
