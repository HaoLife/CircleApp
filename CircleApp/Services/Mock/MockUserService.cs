


namespace CircleApp.Services.Mock;

public class MockUserService : IUserService
{

    public static List<UserDto> Users = new List<UserDto>()
        {
            new UserDto(){
                Id=new Guid("00000000-0000-0000-0000-000000000001"),
                Birthday=new DateOnly(1989,02,11),
                Sex= Sex.Man,
                NickName="耗子",
                Career="软件工程师",
                Education="大专",
                HeadImage="ms-appx:///CircleApp/Assets/default_head.jpg",
                LiveAddress=new RegionAddress("350103","福建省","福州市","台江区"),
                HomeAddress = new RegionAddress("350103", "福建省", "福州市", "台江区"),
                ChildStatus = ChildStatus.Nothing,
                FeelingStatus = FeelingStatus.Alone,
                Height = 170,
                Weight = 120,
                MarriageStatus = MarriageStatus.UnMarried,
                Nation = "汉",
                SocializeTargetStatus = SocializeTargetStatus.ShortTerm,
                Detail=new UserDetailDto()
                {
                    AboutMe="经常外出很少宅在家里，有点理想化，不愿将就。\r\n工作不是很忙",
                    MyVoice="这是我的声音",
                    Hobby="喜欢吃，觉得美食能够治愈心情！休息日会和朋友探店，有时候自己一个人会去跑步爬山，或者在宿舍看书，如果有时间，想去看看祖国大好山河",
                    Feeling="只为遇到你",
                    Loved="等待！",
                }

            },
            new UserDto(){
                Id=new Guid("00000000-0000-0000-0000-000000000002"),
                Birthday=new DateOnly(1989,02,11),
                Sex= Sex.WoMan,
                NickName="狂暴的目猩猩",
                Career="财务",
                Education="大专",
                HeadImage="ms-appx:///CircleApp/Assets/default_head.jpg",
                LiveAddress=new RegionAddress("350103","福建省","福州市","台江区"),
                HomeAddress = new RegionAddress("350103", "福建省", "福州市", "台江区"),
                ChildStatus = ChildStatus.Nothing,
                FeelingStatus = FeelingStatus.Alone,
                Height = 170,
                Weight = 120,
                MarriageStatus = MarriageStatus.UnMarried,
                Nation = "汉",
                SocializeTargetStatus = SocializeTargetStatus.ShortTerm,
                Detail=new UserDetailDto()
                {
                    AboutMe="经常外出很少宅在家里，有点理想化，不愿将就。\r\n工作不是很忙",
                    MyVoice="这是我的声音",
                    Hobby="喜欢吃，觉得美食能够治愈心情！休息日会和朋友探店，有时候自己一个人会去跑步爬山，或者在宿舍看书，如果有时间，想去看看祖国大好山河",
                    Feeling="只为遇到你",
                    Loved="等待！",
                }
            },
            new UserDto(){
                Id=new Guid("00000000-0000-0000-0000-000000000002"),
                Birthday=new DateOnly(1989,02,11),
                Sex= Sex.WoMan,
                NickName="祝福",
                Career="老师",
                Education="本科",
                HeadImage="ms-appx:///CircleApp/Assets/default_head.jpg",
                LiveAddress=new RegionAddress("350103","福建省","福州市","台江区"),
                HomeAddress = new RegionAddress("350103", "福建省", "福州市", "台江区"),
                ChildStatus = ChildStatus.Nothing,
                FeelingStatus = FeelingStatus.Alone,
                Height = 170,
                Weight = 120,
                MarriageStatus = MarriageStatus.UnMarried,
                Nation = "汉",
                SocializeTargetStatus = SocializeTargetStatus.ShortTerm,
                Detail=new UserDetailDto()
                {
                    AboutMe="经常外出很少宅在家里，有点理想化，不愿将就。\r\n工作不是很忙",
                    MyVoice="这是我的声音",
                    Hobby="喜欢吃，觉得美食能够治愈心情！休息日会和朋友探店，有时候自己一个人会去跑步爬山，或者在宿舍看书，如果有时间，想去看看祖国大好山河",
                    Feeling="只为遇到你",
                    Loved="等待！",
                }
            },
            new UserDto(){
                Id=new Guid("00000000-0000-0000-0000-000000000003"),
                Birthday=new DateOnly(1989,02,11),
                Sex= Sex.WoMan,
                NickName="白茶",
                Career="老师",
                Education="本科",
                HeadImage="ms-appx:///CircleApp/Assets/default_head.jpg",
                LiveAddress=new RegionAddress("350103","福建省","福州市","台江区"),
                HomeAddress = new RegionAddress("350103", "福建省", "福州市", "台江区"),
                ChildStatus = ChildStatus.Nothing,
                FeelingStatus = FeelingStatus.Alone,
                Height = 170,
                Weight = 120,
                MarriageStatus = MarriageStatus.UnMarried,
                Nation = "汉",
                SocializeTargetStatus = SocializeTargetStatus.ShortTerm,
                Detail=new UserDetailDto()
                {
                    AboutMe="经常外出很少宅在家里，有点理想化，不愿将就。\r\n工作不是很忙",
                    MyVoice="这是我的声音",
                    Hobby="喜欢吃，觉得美食能够治愈心情！休息日会和朋友探店，有时候自己一个人会去跑步爬山，或者在宿舍看书，如果有时间，想去看看祖国大好山河",
                    Feeling="只为遇到你",
                    Loved="等待！",
                }
            },
        };

    public async ValueTask<LikeStatisticsDto> GetStatisticsLikeAsync(CancellationToken cancellationToken = default)
    {
        return new LikeStatisticsDto()
        {
            BeLikeCount = 99,
            LikeCount = 21,
            VisitCount = 300,
        };
    }

    public async ValueTask<UserDto> GetMeAsync(CancellationToken cancellationToken = default)
    {
        return Users[0];
    }

    public async ValueTask<UserDto> GetRecommendAsync(CancellationToken cancellationToken = default)
    {
        var rm = new Random();
        var i = rm.Next(1, Users.Count) - 1;

        return Users[i];
    }

    public async ValueTask<IImmutableList<UserDto>> GetRecommendsAsync(CancellationToken cancellationToken = default)
    {
        return Users.ToImmutableList();
    }

    public async ValueTask<IImmutableList<UserDto>> GetLikeAsync(CancellationToken cancellationToken = default)
    {
        return Users.OrderBy(a => (new Random()).Next()).ToImmutableList();
    }

    public async ValueTask<IImmutableList<UserDto>> GetBeLikeAsync(CancellationToken cancellationToken = default)
    {
        return Users.OrderBy(a => (new Random()).Next()).ToImmutableList();
    }

    public async ValueTask<IImmutableList<UserDto>> GetVisitAsync(CancellationToken cancellationToken = default)
    {
        return Users.OrderBy(a => (new Random()).Next()).ToImmutableList();
    }
}
