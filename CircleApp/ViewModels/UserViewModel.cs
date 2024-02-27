namespace CircleApp.ViewModels;

public partial class UserViewModel : ObservableObject
{
    private readonly INavigator navigator;
    private readonly IDispatcher dispatcher;

    public UserViewModel(INavigator navigator, IDispatcher dispatcher, UserDto user)
    {
        this.navigator = navigator;
        this.dispatcher = dispatcher;
        User = user;
    }

    public UserDto User { get; }
}
