using System.ComponentModel;

namespace CircleApp.DataContracts;


/// <summary>
/// 用户详情
/// </summary>
public class UserDetailDto
{
    /// <summary>
    /// 关于我
    /// </summary>
    public string AboutMe { get; set; }

    /// <summary>
    /// 我的声音
    /// </summary>
    public string MyVoice { get; set; }

    /// <summary>
    /// 兴趣爱好
    /// </summary>
    public string Hobby { get; set; }

    /// <summary>
    /// 感情观
    /// </summary>
    public string Feeling { get; set; }

    /// <summary>
    /// 心仪的ta
    /// </summary>
    public string Loved { get; set; }
}

/// <summary>
/// 用户简介
/// </summary>
public class UserInfoDto
{
    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    public string HeadImage { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Sex Sex { get; set; }
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateOnly Birthday { get; set; }
    /// <summary>
    /// 居住地
    /// </summary>
    public RegionAddress LiveAddress { get; set; }
    /// <summary>
    /// 学历
    /// </summary>
    public string Education { get; set; }
    /// <summary>
    /// 职业
    /// </summary>
    public string Career { get; set; }
}

/// <summary>
/// 用户
/// </summary>
public class UserDto : UserInfoDto
{
    ///// <summary>
    ///// id
    ///// </summary>
    //public Guid Id { get; set; }
    ///// <summary>
    ///// 头像地址
    ///// </summary>
    //public string HeadImage { get; set; }
    ///// <summary>
    ///// 昵称
    ///// </summary>
    //public string NickName { get; set; }
    ///// <summary>
    ///// 性别
    ///// </summary>
    //public Sex Sex { get; set; }
    ///// <summary>
    ///// 出生日期
    ///// </summary>
    //public DateOnly Birthday { get; set; }
    /// <summary>
    /// 身高
    /// </summary>
    public decimal Height { get; set; }
    /// <summary>
    /// 体重
    /// </summary>
    public decimal Weight { get; set; }
    ///// <summary>
    ///// 居住地
    ///// </summary>
    //public RegionAddress LiveAddress { get; set; }
    /// <summary>
    /// 家乡
    /// </summary>
    public RegionAddress HomeAddress { get; set; }
    /// <summary>
    /// 婚姻状态
    /// </summary>
    public MarriageStatus MarriageStatus { get; set; }
    /// <summary>
    /// 子女状态
    /// </summary>
    public ChildStatus ChildStatus { get; set; }
    /// <summary>
    /// 脱单目标
    /// </summary>
    public SocializeTargetStatus SocializeTargetStatus { get; set; }
    /// <summary>
    /// 感情状态
    /// </summary>
    public FeelingStatus FeelingStatus { get; set; }
    ///// <summary>
    ///// 学历
    ///// </summary>
    //public string Education { get; set; }
    /// <summary>
    /// 民族
    /// </summary>
    public string Nation { get; set; }
    ///// <summary>
    ///// 职业
    ///// </summary>
    //public string Career { get; set; }

    /// <summary>
    /// 详情
    /// </summary>
    public UserDetailDto Detail { get; set; }
}


/// <summary>
/// 性别
/// </summary>
public enum Sex
{
    [Description("男")]
    Man = 0,
    [Description("女")]
    WoMan = 1,
}

/// <summary>
/// 区域地址
/// </summary>
/// <param name="AdminCode">行政编码</param>
/// <param name="Province">省</param>
/// <param name="City">市</param>
/// <param name="County">县</param>
public record RegionAddress(string AdminCode, string Province, string City, string County)
{
    public string Full => $"{this.Province}{this.City}{this.County}";

    public override string ToString()
    {
        return $"{this.AdminCode}:{this.Full}";
    }
}

/// <summary>
/// 婚姻状态
/// </summary>
public enum MarriageStatus
{
    /// <summary>
    /// 未婚
    /// </summary>
    [Description("未婚")]
    UnMarried = 0,
    /// <summary>
    /// 离异
    /// </summary>
    [Description("离异")]
    Divorce = 1,
    /// <summary>
    /// 丧偶
    /// </summary>
    [Description("丧偶")]
    Bereave = 2,
}
/// <summary>
/// 子女状态
/// </summary>
public enum ChildStatus
{
    /// <summary>
    /// 没有
    /// </summary>
    [Description("没有")]
    Nothing = 0,
    /// <summary>
    /// 有
    /// </summary>
    [Description("有")]
    Have = 1,

}
/// <summary>
/// 脱单目标
/// </summary>
public enum SocializeTargetStatus
{
    /// <summary>
    /// 还没考虑清楚
    /// </summary>
    [Description("还没考虑清楚")]
    Pending = 0,
    /// <summary>
    /// 近期
    /// </summary>
    [Description("近期")]
    ShortTerm = 1,
    /// <summary>
    /// 合适时间
    /// </summary>
    [Description("合适时间")]
    Suitable = 2,
    /// <summary>
    /// 先认真谈恋爱
    /// </summary>
    [Description("先认真谈恋爱")]
    OnlyLove = 3,

}
/// <summary>
/// 感情状态
/// </summary>
public enum FeelingStatus
{
    /// <summary>
    /// 单身
    /// </summary>
    [Description("单身")]
    Alone = 0,
    /// <summary>
    /// 已脱单
    /// </summary>
    [Description("已脱单")]
    NotAlone = 1,
}
