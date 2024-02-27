
namespace CircleApp.ViewModels;

public partial class GuideViewModel : ObservableObject
{
    private readonly INavigator navigator;
    private readonly IDispatcher dispatcher;

    public GuideViewModel(INavigator navigator, IDispatcher dispatcher)
    {
        this.navigator = navigator;
        this.dispatcher = dispatcher;
        this.GoToLoginCommand = new AsyncRelayCommand(GoToLogin);
    }

    [ObservableProperty]
    private bool _checked = false;

    public ICommand GoToLoginCommand { get; }


    public async Task GoToLogin()
    {
        var ck = this.Checked;

        if (!ck)
        {
            var cancelSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            await this.navigator.ShowMessageDialogAsync(this,
                content: "请先勾选同意下面的《用户协议》等内容",
                cancellation: cancelSource.Token);

            return;
        }

        await this.navigator.NavigateViewModelAsync<Full>(this);
    }
}
