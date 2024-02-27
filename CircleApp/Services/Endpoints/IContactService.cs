using Refit;

namespace CircleApp.Services.Endpoints;

[Headers("Content-Type: application/json")]
public interface IContactService
{
    [Get("/api/Contact/me")]
    ValueTask<IImmutableList<ContactDto>> GetMyAsync(CancellationToken cancellationToken = default);
}
