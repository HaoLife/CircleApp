namespace CircleApp.ViewModels;
public partial class PrivacyViewModel : ObservableObject
{
    private readonly INavigator navigator;

    public PrivacyViewModel(INavigator navigator)
    {
        this.navigator = navigator;
    }


    //public IState<SystemSettingDto> Settings => State<SystemSettingDto>.Async(this, async (ct) => new SystemSettingDto());

    [ObservableProperty]
    private SystemSettingDto? _settings;

}
