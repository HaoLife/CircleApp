namespace CircleApp.ViewModels;

public partial class TrendViewModel : ObservableObject
{
    private readonly ITrendService trendService;

    public TrendViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        ITrendService trendService,
        TrendDto entity)
    {
        this.trendService = trendService;
        this.Entity = entity;
        _ = Init();
    }

    private async Task Init()
    {
        this.Comments = new ObservableCollection<TrendCommentDto>(await trendService.GetCommentsAsync(this.Entity.Id));
        this.Statistics = await trendService.GetLikesAsync(this.Entity.Id);
    }
    public TrendDto Entity { get; }

    [ObservableProperty]
    private StatisticsUser? _statistics;

    [ObservableProperty]
    private ObservableCollection<TrendCommentDto> _comments = new ObservableCollection<TrendCommentDto>();
}
