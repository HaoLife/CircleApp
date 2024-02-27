namespace CircleApp.Models;

public partial record LikeModel
{
    private INavigator navigator;
    private readonly IUserService userService;
    private readonly SelectInt select;

    public LikeModel(
        INavigator navigator,
        IUserService userService,
        SelectInt select)
    {
        this.navigator = navigator;
        this.userService = userService;
        this.select = select;
        _ = Init();
    }

    public async Task Init()
    {
        await this.Selected.UpdateAsync(a => select.Type);
    }


    public IListFeed<UserDto> Likes => ListFeed.Async(userService.GetLikeAsync);
    public IListFeed<UserDto> BeLikes => ListFeed.Async(userService.GetBeLikeAsync);
    public IListFeed<UserDto> Visits => ListFeed.Async(userService.GetVisitAsync);

    public IState<int> Selected => State.Value(this, () => -1);



    public async Task GoToUser(UserDto user)
    {
        await this.navigator.NavigateViewModelAsync<UserModel>(this, data: user);
    }

}
