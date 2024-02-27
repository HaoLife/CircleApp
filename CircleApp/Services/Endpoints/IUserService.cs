using Refit;

namespace CircleApp.Services.Endpoints;


[Headers("Content-Type: application/json")]
public interface IUserService
{
    [Get("/api/Users/me")]
    ValueTask<UserDto> GetMeAsync(CancellationToken cancellationToken = default);


    [Get("/api/Users/statistic")]
    ValueTask<LikeStatisticsDto> GetStatisticsLikeAsync(CancellationToken cancellationToken = default);


    [Get("/api/Users/recommend")]
    ValueTask<UserDto> GetRecommendAsync(CancellationToken cancellationToken = default);


    [Get("/api/Users/recommends")]
    ValueTask<IImmutableList<UserDto>> GetRecommendsAsync(CancellationToken cancellationToken = default);


    [Get("/api/Users/like")]
    ValueTask<IImmutableList<UserDto>> GetLikeAsync(CancellationToken cancellationToken = default);

    [Get("/api/Users/belike")]
    ValueTask<IImmutableList<UserDto>> GetBeLikeAsync(CancellationToken cancellationToken = default);
    //VisitCount
    [Get("/api/Users/visit")]
    ValueTask<IImmutableList<UserDto>> GetVisitAsync(CancellationToken cancellationToken = default);
}
