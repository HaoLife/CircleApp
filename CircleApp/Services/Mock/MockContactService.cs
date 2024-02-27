

namespace CircleApp.Services.Mock;

public class MockContactService : IContactService
{
    public async ValueTask<IImmutableList<ContactDto>> GetMyAsync(CancellationToken cancellationToken = default)
    {
        var list = new List<ContactDto>()
        {
            new ContactDto()
            {
                Id=Guid.NewGuid(),
                SourceId= MockUserService.Users[0].Id,
                AddTime= DateTime.Now,
                AddSource= ContactAddSource.Search,
                Mode= ContactMode.Single,
                ContactId= MockUserService.Users[1].Id,
                ContactUser=MockUserService.Users[1],
                LastMessage="测试消息",

            },
            new ContactDto()
            {
                Id=Guid.NewGuid(),
                SourceId= MockUserService.Users[0].Id,
                AddTime= DateTime.Now,
                AddSource= ContactAddSource.Search,
                Mode= ContactMode.Single,
                ContactId= MockUserService.Users[2].Id,
                ContactUser=MockUserService.Users[2],
                LastMessage="测试消息1",

            },
            new ContactDto()
            {
                Id=Guid.NewGuid(),
                SourceId= MockUserService.Users[0].Id,
                AddTime= DateTime.Now,
                AddSource= ContactAddSource.Search,
                Mode= ContactMode.Single,
                ContactId= MockUserService.Users[3].Id,
                ContactUser=MockUserService.Users[3],
                LastMessage="测试消息2",

            },
            new ContactDto()
            {
                Id=Guid.NewGuid(),
                SourceId= MockUserService.Users[0].Id,
                AddTime= DateTime.Now,
                AddSource= ContactAddSource.Activity,
                Mode= ContactMode.Activity,
                ContactId= new Guid("00000000-0000-0000-0000-000000000001"),
                LastMessage="测试消息2",

            },
        };

        return list.ToImmutableList();
    }
}
