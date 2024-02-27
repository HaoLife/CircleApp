using System.ComponentModel;

namespace CircleApp.DataContracts;

/// <summary>
/// 联系
/// </summary>
public class ContactDto
{
    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 所有者id
    /// </summary>
    public Guid SourceId { get; set; }
    /// <summary>
    /// 联系id
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime? AddTime { get; set; }

    /// <summary>
    /// 联系添加来源
    /// </summary>
    public ContactAddSource AddSource { get; set; }

    /// <summary>
    /// 联系模式
    /// </summary>
    public ContactMode Mode { get; set; }

    /// <summary>
    /// 最后一条消息
    /// </summary>
    public string LastMessage { get; set; }


    /// <summary>
    /// 联系人简介信息
    /// </summary>
    public UserInfoDto ContactUser { get; set; }

}



/// <summary>
/// 联系添加来源
/// </summary>
[Description("联系添加来源")]
public enum ContactAddSource
{
    /// <summary>
    /// 寻觅
    /// </summary>
    [Description("寻觅")]
    Search = 0,
    /// <summary>
    /// 动态
    /// </summary>
    [Description("动态")]
    Trend = 1,
    /// <summary>
    /// 活动
    /// </summary>
    [Description("活动")]
    Activity = 2,

}

/// <summary>
/// 联系模式
/// </summary>
[Description("联系模式")]
public enum ContactMode
{
    /// <summary>
    /// 单人
    /// </summary>
    [Description("单人")]
    Single = 0,
    /// <summary>
    /// 活动
    /// </summary>
    [Description("活动")]
    Activity = 1,
}
