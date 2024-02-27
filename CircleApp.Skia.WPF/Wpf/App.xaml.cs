using Uno.UI.Runtime.Skia.Wpf;
using WpfApp = System.Windows.Application;

namespace CircleApp.WPF;
public partial class App : WpfApp
{
    public App()
    {
        var host = new WpfHost(Dispatcher, () => new AppHead());
        host.Run();
    }
}
