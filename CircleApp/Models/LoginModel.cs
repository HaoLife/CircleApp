namespace CircleApp.Models;

public partial record LoginModel
{
    private readonly IDispatcher dispatcher;

    public LoginModel(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;

    }


    public IState<string> Username => State<string>.Value(this, () => string.Empty);

    public IState<string> Password => State<string>.Value(this, () => string.Empty);

}
