
namespace CircleApp.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MessagePage : Page
{
    public MessagePage()
    {
        this.InitializeComponent();
    }

    private void TextBox_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter && this.DataContext is MessageViewModel vm)
        {
            var v = sender as TextBox;
            vm.PublishCommand.Execute(v.Text);
            v.Text = string.Empty;
        }
    }
}
