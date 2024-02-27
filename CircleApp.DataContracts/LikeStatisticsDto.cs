
namespace CircleApp.DataContracts;

/// <summary>
/// 喜欢统计
/// </summary>
public class LikeStatisticsDto
{
    /// <summary>
    /// 喜欢数量
    /// </summary>
    public int LikeCount { get; set; }
    /// <summary>
    /// 被喜欢数量
    /// </summary>
    public int BeLikeCount { get; set; }
    /// <summary>
    /// 访问数量
    /// </summary>
    public int VisitCount { get; set; }
}

