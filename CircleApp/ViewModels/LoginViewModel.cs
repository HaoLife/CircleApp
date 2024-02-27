namespace CircleApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IDispatcher dispatcher;

    public LoginViewModel(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;

    }


    //public IState<string> Username => State<string>.Value(this, () => string.Empty);

    //public IState<string> Password => State<string>.Value(this, () => string.Empty);

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

}
