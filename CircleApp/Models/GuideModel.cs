namespace CircleApp.Models;

public partial record GuideModel
{
    private readonly INavigator navigator;
    private readonly IDispatcher dispatcher;

    public GuideModel(INavigator navigator, IDispatcher dispatcher)
    {
        this.navigator = navigator;
        this.dispatcher = dispatcher;
    }
    public IState<bool> Checked => State<bool>.Value(this, () => false);



    public async Task GoToLogin()
    {
        var ck = await this.Checked;

        if (!ck)
        {
            var cancelSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            await this.navigator.ShowMessageDialogAsync(this,
                content: "请先勾选同意下面的《用户协议》等内容",
                cancellation: cancelSource.Token);


            //var t = new ContentDialog()
            //{
            //    Title = "测试",
            //    Content = "我的内容",
            //};


            //Task.Factory.StartNew(async () =>
            //{
            //    await Task.Delay(500);

            //    t.Hide();
            //});

            // await t.ShowAsync();

            return;
        }

        await this.navigator.NavigateViewModelAsync<Full>(this);
    }
}
