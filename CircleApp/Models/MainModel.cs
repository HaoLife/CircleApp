using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace CircleApp.Models;

public partial record MainModel
{
    private INavigator navigator;
    private readonly ITrendService trendService;
    private readonly IContactService contactService;
    private readonly IUserService userService;

    public MainModel(
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
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";

        //_ = Init();
    }
    //private async Task Init()
    //{
    //    this.Recommends = new ObservableCollection<UserDto>(await this.userService.GetRecommendsAsync(CancellationToken.None));
    //}


    public string? Title { get; }

    public IFeed<UserDto> Me => Feed.Async(userService.GetMeAsync);
    public IFeed<LikeStatisticsDto> Like => Feed.Async(userService.GetStatisticsLikeAsync);

    public IListFeed<ContactDto> Contacts => ListFeed.Async(contactService.GetMyAsync);


    //public IListState<TrendDto> Trends => ListState.Async(this, async (ct) => await this.trendService.GetSearchAsync(await this.Search));

    public IState<TrendSearch> Search => State.Value(this, () => new TrendSearch() { TrendType = TrendType.SameCity });



    public IListState<TrendDto> CareTrends => ListState.Async(this, async (ct) => await this.trendService.GetTrendTypeAsync(TrendType.Care));
    public IListState<TrendDto> NewTrends => ListState.Async(this, async (ct) => await this.trendService.GetTrendTypeAsync(TrendType.New));
    public IListState<TrendDto> SameCityTrends => ListState.Async(this, async (ct) => await this.trendService.GetTrendTypeAsync(TrendType.SameCity));


    // System.Text.Encoding.Unicode.GetString(BitConverter.GetBytes(x));

    public IState<UserDto> Recommend => State.Async(this, GetRecommendAsync);


    //public ObservableCollection<UserDto> Recommends { get; set; } = new ObservableCollection<UserDto>();


    private BlockingCollection<UserDto> _recommendQueues = new BlockingCollection<UserDto>();


    public async Task UpdateItemTrends(string value, CancellationToken ct)
    {
        var type = (TrendType)Convert.ToInt32(value);
        var data = await this.trendService.GetTrendTypeAsync(type);
        switch (type)
        {
            case TrendType.Care:
                await this.CareTrends.UpdateAsync(a => data, ct);
                return;
            case TrendType.SameCity:
                await this.SameCityTrends.UpdateAsync(a => data, ct);
                return;
            default:
                await this.NewTrends.UpdateAsync(a => data, ct);
                return;
        }


    }


    public async Task HandleRecommend(bool isLike)
    {
        var c = CancellationToken.None;
        var model = await GetRecommendAsync(c);
        await this.Recommend.Update(a => model, c);

    }


    public async ValueTask<UserDto> GetRecommendAsync(CancellationToken cancellationToken)
    {
        if (_recommendQueues.Count <= 3)
        {
            var ls = await userService.GetRecommendsAsync(cancellationToken);

            foreach (var item in ls)
                this._recommendQueues.Add(item);
        }
        return _recommendQueues.Take();

    }

    //public async Task UpdateTrends(CancellationToken ct)
    //{
    //    var search = await this.Search;
    //    var values = await this.trendService.GetSearchAsync(search);

    //    //await this.Trends.Update(a => values, ct);

    //    await this.Trends.UpdateAsync(a => values, ct);

    //}

    public async Task GoToTrend(TrendDto trend)
    {
        await this.navigator.NavigateViewModelAsync<TrendModel>(this, data: trend);

        //var ls = await Trends;

        //var d = await SelectedTrend;


        //Console.WriteLine($"{ls.IndexOf(trend)} - {trend.Content} : {(d == null ? -1 : ls.IndexOf(d))} - {d?.Content}");
    }

    //public async Task GoToSecond()
    //{
    //    //await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));

    //}

    public async Task GetMessage(ContactDto message)
    {

        await this.navigator.NavigateViewModelAsync<MessageModel>(this, data: message);
    }


    public async Task GoToRoute(string name)
    {
        await this.navigator.NavigateRouteAsync(this, name);
    }

    public async Task GoToLike(string value)
    {
        await this.navigator.NavigateViewModelAsync<LikeModel>(this, data: new SelectInt(Convert.ToInt32(value)));
    }

}
