using Microsoft.UI;

namespace CircleApp.Models;

public partial record MessageModel
{
    private readonly IUserService userService;
    private readonly IMessageRecordService messageRecordService;

    public MessageModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        IUserService userService,
        IMessageRecordService messageRecordService,
        ContactDto contact)
    {
        this.userService = userService;
        this.messageRecordService = messageRecordService;
        Contact = contact;

        _ = Init();
    }

    public UserInfoDto Sender { get; set; }

    public async Task Init()
    {
        var user = await this.userService.GetMeAsync(CancellationToken.None);
        Sender = new UserInfoDto
        {
            Id = user.Id,
            Birthday = user.Birthday,
            Career = user.Career,
            LiveAddress = user.LiveAddress,
            Education = user.Education,
            HeadImage = user.HeadImage,
            NickName = user.NickName,
            Sex = user.Sex
        };
    }

    public IListState<MessageRecordWrap> Messages => ListState.Async(this, async (ct) =>
    {
        var messages = await messageRecordService.GetContactMessageAsync(this.Contact.ContactId);
        // new MessageRecordWrap(new MessageRecord() { Message = "你好", SenderId = Sender.Id, ReceiverId = Receiver.Id, Sender = Sender, SendTime = DateTime.Now }, this.Contact),
        return messages.Select(a => new MessageRecordWrap(a, this.Contact)).ToImmutableList();

    });

    public ContactDto Contact { get; }

    public IState<string> PublishMessage => State<string>.Empty(this);
    public async Task Publish()
    {
        var message = await this.PublishMessage;
        await this.PublishMessage.Update(a => string.Empty, CancellationToken.None);


        await this.Messages.Update(a =>
        {
            var ls = new List<MessageRecordWrap>(a);
            ls.Add(new MessageRecordWrap(new MessageRecordDto()
            {
                Message = message,
                Sender = Sender,
                ReceiverId = this.Contact.ContactId,
                SendTime = DateTime.Now
            }, this.Contact));

            return ls.ToImmutableList();
        }, CancellationToken.None);
    }

}

