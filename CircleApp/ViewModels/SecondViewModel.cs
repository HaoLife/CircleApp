namespace CircleApp.ViewModels;
public partial class SecondViewModel : ObservableObject
{
    private readonly INavigator navigator;

    public SecondViewModel(INavigator navigator)
    {
        this.navigator = navigator;
    }
}
