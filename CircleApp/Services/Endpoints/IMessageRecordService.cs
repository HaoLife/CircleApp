using Refit;

namespace CircleApp.Services.Endpoints;

[Headers("Content-Type: application/json")]
public interface IMessageRecordService
{
    [Get("/api/MessageRecord/contact")]
    ValueTask<IImmutableList<MessageRecordDto>> GetContactMessageAsync(Guid contactId, CancellationToken cancellationToken = default);
}
