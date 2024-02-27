namespace CircleApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private INavigator navigator;
    private readonly ITrendService trendService;
    private readonly IContactService contactService;
    private readonly IUserService userService;

    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        ITrendService trendService,
        IContactService contactService,
        IUserService userService)
    {
        this.navigator = navigator;
        this.trendService = trendService;
        this.contactService = contactService;
        this.userService = userService;


        this.UpdateItemTrendsCommand = new AsyncRelayCommand<int>(UpdateItemTrends);
        this.HandleRecommendCommand = new AsyncRelayCommand<bool>(HandleRecommend);
        this.GoToTrendCommand = new AsyncRelayCommand<TrendDto>(GoToTrend);
        this.GetMessageCommand = new AsyncRelayCommand<ContactDto>(GetMessage);
        this.GoToRouteCommand = new AsyncRelayCommand<string>(GoToRoute);
        this.GoToLikeCommand = new AsyncRelayCommand<string>(GoToLike);
        _ = Init();
    }
    private async Task Init()
    {
        this.Me = await this.userService.GetMeAsync();
        this.Like = await this.userService.GetStatisticsLikeAsync();

        this.Contacts = new ObservableCollection<ContactDto>(await this.contactService.GetMyAsync());
        this.CareTrends = new ObservableCollection<TrendDto>(await this.trendService.GetTrendTypeAsync(TrendType.Care));
        this.NewTrends = new ObservableCollection<TrendDto>(await this.trendService.GetTrendTypeAsync(TrendType.New));
        this.SameCityTrends = new ObservableCollection<TrendDto>(await this.trendService.GetTrendTypeAsync(TrendType.SameCity));

        this.Recommend = await this.GetRecommendAsync();
    }


    [ObservableProperty]
    private UserDto? _me;

    [ObservableProperty]
    private LikeStatisticsDto? _like;

    [ObservableProperty]
    private ObservableCollection<ContactDto> _contacts = new ObservableCollection<ContactDto>();

    [ObservableProperty]
    private ObservableCollection<TrendDto> _careTrends = new ObservableCollection<TrendDto>();
    [ObservableProperty]
    private ObservableCollection<TrendDto> _newTrends = new ObservableCollection<TrendDto>();
    [ObservableProperty]
    private ObservableCollection<TrendDto> _sameCityTrends = new ObservableCollection<TrendDto>();

    [ObservableProperty]
    private UserDto? _recommend;

    private BlockingCollection<UserDto> _recommendQueues = new BlockingCollection<UserDto>();


    public ICommand UpdateItemTrendsCommand { get; }
    public ICommand HandleRecommendCommand { get; }
    public ICommand GoToTrendCommand { get; }
    public ICommand GetMessageCommand { get; }
    public ICommand GoToRouteCommand { get; }
    public ICommand GoToLikeCommand { get; }


    public async Task UpdateItemTrends(int value, CancellationToken ct)
    {
        var type = (TrendType)value;
        var data = await this.trendService.GetTrendTypeAsync(type);
        switch (type)
        {
            case TrendType.Care:
                this.CareTrends = new ObservableCollection<TrendDto>(data);
                return;
            case TrendType.SameCity:
                this.SameCityTrends = new ObservableCollection<TrendDto>(data);
                return;
            default:
                this.NewTrends = new ObservableCollection<TrendDto>(data);
                return;
        }


    }


    public async Task HandleRecommend(bool isLike)
    {
        var c = CancellationToken.None;
        var model = await GetRecommendAsync(c);
        this.Recommend = model;

    }


    public async ValueTask<UserDto> GetRecommendAsync(CancellationToken cancellationToken = default)
    {
        if (_recommendQueues.Count <= 3)
        {
            var ls = await userService.GetRecommendsAsync(cancellationToken);

            foreach (var item in ls)
                this._recommendQueues.Add(item);
        }
        return _recommendQueues.Take();

    }

    public async Task GoToTrend(TrendDto? trend)
    {
        await this.navigator.NavigateViewModelAsync<TrendViewModel>(this, data: trend);

    }

    public async Task GetMessage(ContactDto? message)
    {

        await this.navigator.NavigateViewModelAsync<MessageViewModel>(this, data: message);
    }


    public async Task GoToRoute(string? name)
    {
        await this.navigator.NavigateRouteAsync(this, name);
    }

    public async Task GoToLike(string? value)
    {
        await this.navigator.NavigateViewModelAsync<LikeViewModel>(this, data: new SelectInt(Convert.ToInt32(value)));
    }

}
