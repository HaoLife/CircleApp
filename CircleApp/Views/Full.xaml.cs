
namespace CircleApp.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Full : Page
{
    public Full()
    {
        this.InitializeComponent();
    }

    private void ContentControl_Loaded(object sender, RoutedEventArgs e)
    {
        var a = sender as ContentControl;
        a.Navigator()?.NavigateRouteAsync(this, "Main");
    }
}
