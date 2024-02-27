
namespace CircleApp.Services.Mock;

public class MockMessageRecordService : IMessageRecordService
{
    public async ValueTask<IImmutableList<MessageRecordDto>> GetContactMessageAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        var message = new List<string>()
        {
            "m:你好",
            "y:你好!",
            "m:你哪里人",
            "m:福州，你呢",
            "y:我也是福州人",
            "m:你是什么职业",
            "y:做老师的",
        };

        var ls = new List<MessageRecordDto>();

        foreach (var item in message)
        {
            var kv = item.Split(":");
            ls.Add(new MessageRecordDto()
            {
                Sender = kv[0].Equals("m") ? MockUserService.Users[0] : MockUserService.Users[1],
                Message = kv[1],
                Mode = MessageMode.Text,
                ReceiverId = !kv[0].Equals("m") ? MockUserService.Users[0].Id : MockUserService.Users[1].Id,
                SendTime = DateTime.Now.AddSeconds(100),
            }
            );
        }


        return ls.ToImmutableList();
    }
}
