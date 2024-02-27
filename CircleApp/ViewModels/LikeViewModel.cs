

namespace CircleApp.ViewModels;

public partial class LikeViewModel : ObservableObject
{
    private INavigator navigator;
    private readonly IUserService userService;
    private readonly SelectInt select;

    public LikeViewModel(
        INavigator navigator,
        IUserService userService,
        SelectInt select)
    {
        this.navigator = navigator;
        this.userService = userService;
        this.select = select;
        this.GoToUserCommand = new AsyncRelayCommand<UserDto>(GoToUser);
        _ = Init();
    }

    public async Task Init()
    {
        this.Selected = select.Type;
        this.Likes = new ObservableCollection<UserDto>(await userService.GetLikeAsync());
        this.BeLikes = new ObservableCollection<UserDto>(await userService.GetBeLikeAsync());
        this.Visits = new ObservableCollection<UserDto>(await userService.GetVisitAsync());
    }

    [ObservableProperty]
    private int _selected = -1;

    [ObservableProperty]
    private ObservableCollection<UserDto> _likes = new ObservableCollection<UserDto>();
    [ObservableProperty]
    private ObservableCollection<UserDto> _beLikes = new ObservableCollection<UserDto>();
    [ObservableProperty]
    private ObservableCollection<UserDto> _visits = new ObservableCollection<UserDto>();

    public ICommand GoToUserCommand { get; }


    public async Task GoToUser(UserDto? user)
    {
        await this.navigator.NavigateViewModelAsync<UserViewModel>(this, data: user);
    }

}
