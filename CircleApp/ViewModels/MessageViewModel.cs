namespace CircleApp.ViewModels;
public partial class MessageViewModel : ObservableObject
{
    private readonly INavigator navigator;
    private readonly IUserService userService;
    private readonly IMessageRecordService messageRecordService;

    public MessageViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        IUserService userService,
        IMessageRecordService messageRecordService,
        ContactDto contact)
    {
        this.navigator = navigator;
        this.userService = userService;
        this.messageRecordService = messageRecordService;
        Contact = contact;
        this.PublishCommand = new AsyncRelayCommand<string>(Publish);
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

        var messages = await messageRecordService.GetContactMessageAsync(this.Contact.ContactId);

        this.Messages = new ObservableCollection<MessageRecordWrap>(messages.Select(a => new MessageRecordWrap(a, this.Contact)));

    }

    [ObservableProperty]
    private ObservableCollection<MessageRecordWrap> _messages = new ObservableCollection<MessageRecordWrap>();

    [ObservableProperty]
    private string _publishMessage = string.Empty;
    public ContactDto Contact { get; }

    public ICommand PublishCommand { get; }

    public async Task Publish(string msg)
    {
        if (string.IsNullOrEmpty(msg)) return;

        var ms = this.Messages.ToList();

        ms.Add(new MessageRecordWrap(new MessageRecordDto()
        {
            Message = msg,
            Sender = this.Sender,
            ReceiverId = this.Contact.ContactId,
            SendTime = DateTime.Now
        }, this.Contact));

        this.Messages = new ObservableCollection<MessageRecordWrap>(ms);

        this.PublishMessage = string.Empty;
    }

}

