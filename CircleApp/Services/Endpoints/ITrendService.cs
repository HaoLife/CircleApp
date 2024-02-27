using Refit;

namespace CircleApp.Services.Endpoints;

[Headers("Content-Type: application/json")]
public interface ITrendService
{

    [Get("/api/Trends/search")]
    ValueTask<IImmutableList<TrendDto>> GetSearchAsync(TrendSearch search, CancellationToken cancellationToken = default);


    [Get("/api/Trends/type")]
    ValueTask<IImmutableList<TrendDto>> GetTrendTypeAsync(TrendType trendType, CancellationToken cancellationToken = default);

    [Get("/api/Trends/commends/{id}")]
    ValueTask<IImmutableList<TrendCommentDto>> GetCommentsAsync([AliasAs("id")]Guid id, CancellationToken cancellationToken = default);


    [Get("/api/Trends/likes/{id}")]
    ValueTask<StatisticsUser> GetLikesAsync([AliasAs("id")] Guid id, CancellationToken cancellationToken = default);
}
