namespace CircleApp.Models;

public partial record UserModel
{
    private readonly INavigator navigator;
    private readonly IDispatcher dispatcher;

    public UserModel(INavigator navigator, IDispatcher dispatcher, UserDto user)
    {
        this.navigator = navigator;
        this.dispatcher = dispatcher;
        User = user;
    }

    public UserDto User { get; }
}
