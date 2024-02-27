namespace CircleApp.Models;
public partial record PrivacyModel
{
    private readonly INavigator navigator;

    public PrivacyModel(INavigator navigator)
    {
        this.navigator = navigator;
    }


    public IState<SystemSettingDto> Settings => State<SystemSettingDto>.Async(this, async (ct) => new SystemSettingDto());

}
