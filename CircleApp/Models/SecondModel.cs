namespace CircleApp.Models;
public partial record SecondModel
{
    private readonly INavigator navigator;

    public SecondModel(INavigator navigator)
    {
        this.navigator = navigator;
    }
}
