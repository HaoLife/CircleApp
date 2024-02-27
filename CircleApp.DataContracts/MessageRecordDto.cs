
using System.ComponentModel;

namespace CircleApp.DataContracts;

/// <summary>
/// 聊天记录
/// </summary>
public class MessageRecordDto
{
    /// <summary>
    /// 发送人
    /// </summary>
    public UserInfoDto Sender { get; set; }
    /// <summary>
    /// 发送内容
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 接收者id （也可能时群id）
    /// </summary>
    public Guid ReceiverId { get; set; }
    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }

    /// <summary>
    /// 消息模式
    /// </summary>
    public MessageMode Mode { get; set; }
}


/// <summary>
/// 消息模式
/// </summary>
public enum MessageMode
{
    /// <summary>
    /// 图片
    /// </summary>
    [Description("图片")]
    Text = 0,
    /// <summary>
    /// 图片
    /// </summary>
    [Description("图片")]
    Picture = 1,
}
