namespace CircleApp.Models;

public partial record TrendModel
{
    private readonly ITrendService trendService;

    public TrendModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        ITrendService trendService,
        TrendDto entity)
    {
        this.trendService = trendService;
        this.Entity = entity;
    }

    public TrendDto Entity { get; }
    public IListFeed<TrendCommentDto> Comments => ListFeed.Async(async ct => await trendService.GetCommentsAsync(this.Entity.Id, ct));


    public IFeed<StatisticsUser> Statistics => Feed.Async(async ct => await trendService.GetLikesAsync(this.Entity.Id, ct));

}
