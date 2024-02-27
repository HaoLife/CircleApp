using System.ComponentModel;

namespace CircleApp.DataContracts;

/// <summary>
/// 动态
/// </summary>
public class TrendDto
{
    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// 图片集合
    /// </summary>
    public List<string> Pictures { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime PublishTime { get; set; }

    /// <summary>
    /// 点赞数
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// 发布人简介
    /// </summary>
    public UserInfoDto Publisher { get; set; }
}




/// <summary>
/// 动态评论
/// </summary>
public class TrendCommentDto
{
    /// <summary>
    /// 动态评论id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }


    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime PublishTime { get; set; }
    /// <summary>
    /// 点赞数
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// 评论人简介
    /// </summary>
    public UserInfoDto Commenter { get; set; }

    /// <summary>
    /// 回复
    /// </summary>
    public List<TrendCommentReplyDto> Replys { get; set; } = new List<TrendCommentReplyDto>();
}

/// <summary>
/// 动态评论回复
/// </summary>
public class TrendCommentReplyDto
{

    /// <summary>
    /// 回复id
    /// </summary>
    public Guid Id { get; set; }

    ///// <summary>
    ///// 动态评论id
    ///// </summary>
    //public Guid TrendCommentId { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }


    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime PublishTime { get; set; }
    /// <summary>
    /// 点赞数
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// 评论人
    /// </summary>
    public UserInfoDto Commenter { get; set; }

    /// <summary>
    /// 回复人
    /// </summary>
    public UserInfoDto Recover { get; set; }

}


/// <summary>
/// 动态查询
/// </summary>
public class TrendSearch
{
    /// <summary>
    /// 动态类型
    /// </summary>
    public TrendType TrendType { get; set; }
}


/// <summary>
/// 动态类型
/// </summary>
public enum TrendType
{
    /// <summary>
    /// 关注
    /// </summary>
    [Description("关注")]
    Care = 0,
    /// <summary>
    /// 最新
    /// </summary>
    [Description("最新")]
    New = 1,
    /// <summary>
    /// 同城
    /// </summary>
    [Description("同城")]
    SameCity = 2,
}


public class StatisticsUser
{
    public int Count { get; set; }
    public List<UserInfoDto> Users { get; set; }
}
