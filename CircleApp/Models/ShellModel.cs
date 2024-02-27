namespace CircleApp.Models;

public class ShellModel
{
    private readonly IAuthenticationService authentication;
    private readonly INavigator _navigator;

    public ShellModel(
        IAuthenticationService authentication,
        INavigator navigator)
    {
        this.authentication = authentication;
        _navigator = navigator;
        this.authentication.LoggedOut += LoggedOut;
        //_ = Start();
    }

    //public async Task Start()
    //{
    //    await _navigator.NavigateRouteAsync(this, "Full");
    //    //await _navigator.NavigateViewModelAsync<MainModel>(this);
    //    //await _navigator.NavigateViewModelAsync<GuideModel>(this);
    //    //await _navigator.NavigateViewModelAsync<PrivacySettingModel>(this);
    //}


    private async void LoggedOut(object? sender, EventArgs e)
    {
        await _navigator.NavigateViewModelAsync<LoginModel>(this, qualifier: Qualifiers.ClearBackStack);
    }
}
