
using Refit;
using System.Linq;

namespace CircleApp.Services.Mock;

public class MockTrendService : ITrendService
{
    public async ValueTask<IImmutableList<TrendCommentDto>> GetCommentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var ls = new List<TrendCommentDto>()
        {
            new TrendCommentDto()
            {
                Id= Guid.NewGuid(),
                Content= "这是出的哪个番的角色啊",
                Commenter = MockUserService.Users[1],
                LikeCount= 1,
                PublishTime= DateTime.Now,
                Replys= new List<TrendCommentReplyDto>()
                {
                    new TrendCommentReplyDto()
                    {
                        Id= Guid.NewGuid(),
                        Content="终结的炽天使",
                        Commenter= MockUserService.Users[0],
                        LikeCount=0,
                        PublishTime= DateTime.Now,
                        Recover =MockUserService.Users[1],
                    },
                    new TrendCommentReplyDto()
                    {
                        Id= Guid.NewGuid(),
                        Content="cooool",
                        Commenter= MockUserService.Users[1],
                        LikeCount=0,
                        PublishTime= DateTime.Now,
                        Recover =MockUserService.Users[0],
                    },
                    new TrendCommentReplyDto()
                    {
                        Id= Guid.NewGuid(),
                        Content="围观",
                        Commenter= MockUserService.Users[2],
                        LikeCount=0,
                        PublishTime= DateTime.Now,
                    },
                }
            },
            new TrendCommentDto()
            {
                Id= Guid.NewGuid(),
                Content= "这是出的哪个番的角色啊",
                Commenter = MockUserService.Users[1],
                LikeCount= 1,
                PublishTime= DateTime.Now,
                Replys= new List<TrendCommentReplyDto>()
                {
                    new TrendCommentReplyDto()
                    {
                        Id= Guid.NewGuid(),
                        Content="终结的炽天使",
                        Commenter= MockUserService.Users[0],
                        LikeCount=0,
                        PublishTime= DateTime.Now,
                        Recover =MockUserService.Users[1],
                    },
                    new TrendCommentReplyDto()
                    {
                        Id= Guid.NewGuid(),
                        Content="cooool",
                        Commenter= MockUserService.Users[1],
                        LikeCount=0,
                        PublishTime= DateTime.Now,
                        Recover =MockUserService.Users[0],
                    },
                    new TrendCommentReplyDto()
                    {
                        Id= Guid.NewGuid(),
                        Content="围观",
                        Commenter= MockUserService.Users[2],
                        LikeCount=0,
                        PublishTime= DateTime.Now,
                    },
                }
            },
        };

        return ls.ToImmutableList();
    }

    public async ValueTask<StatisticsUser> GetLikesAsync([AliasAs("id")] Guid id, CancellationToken cancellationToken = default)
    {
        var ls = new List<UserInfoDto>()
        {
            MockUserService.Users[0],
            MockUserService.Users[1],
            MockUserService.Users[2],
            MockUserService.Users[3],
        };

        return new StatisticsUser { Count = 210, Users = ls };
    }

    public async ValueTask<IImmutableList<TrendDto>> GetSearchAsync(TrendSearch search, CancellationToken cancellationToken = default)
    {
        var mod = new List<TrendDto>()
        {
            new TrendDto(){
                Id=Guid.NewGuid(),
                LikeCount=100,
                Content="发现自己把快乐和幸福弄丢了，我所谓的快乐和幸福就是亲情",
                Pictures=new List<string>(){ "ms-appx:///CircleApp/Assets/default_head.jpg" },
                PublishTime=DateTime.Now,
                Publisher= MockUserService.Users[0],
            },
            new TrendDto(){
                Id=Guid.NewGuid(),
                LikeCount=30,
                Content="以前的样子很可爱啊",
                Pictures=new List<string>(){ "ms-appx:///CircleApp/Assets/default_head.jpg" },
                PublishTime=DateTime.Now,
                Publisher= MockUserService.Users[1],
            },
            new TrendDto(){
                Id=Guid.NewGuid(),
                LikeCount=90,
                Content="以前的样子很可爱啊,test1",
                Pictures=new List<string>(){ "ms-appx:///CircleApp/Assets/default_head.jpg" },
                PublishTime=DateTime.Now,
                Publisher= MockUserService.Users[2],
            },
        };

        var list = new List<TrendDto>();

        var v = (new Random()).Next(2, 9);
        for (var i = 0; i < v; i++)
        {
            var temp = mod.ElementAt((new Random()).Next(1, mod.Count) - 1);
            var d = new TrendDto()
            {
                Id = Guid.NewGuid(),
                LikeCount = temp.LikeCount,
                Content = $"{temp.Content}_{i}",
                Pictures = temp.Pictures,
                Publisher = temp.Publisher,
                PublishTime = temp.PublishTime
            };
            list.Add(d);
        }

        return list.ToImmutableList();
    }

    public ValueTask<IImmutableList<TrendDto>> GetTrendTypeAsync(TrendType trendType, CancellationToken cancellationToken = default)
    {
        return this.GetSearchAsync(new TrendSearch() { TrendType = trendType }, cancellationToken);
    }
}
