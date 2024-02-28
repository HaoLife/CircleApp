using Windows.ApplicationModel.Contacts;

namespace CircleApp;

public class App : Microsoft.UI.Xaml.Application
{
    protected Window? MainWindow { get; private set; }
    protected IHost? Host { get; private set; }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            // Add navigation support for toolkit controls such as TabBar and NavigationView
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    // Configure log levels for different categories of logging
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ?
                                LogLevel.Information :
                                LogLevel.Warning)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);

                    // Uno Platform namespace filter groups
                    // Uncomment individual methods to see more detailed logging
                    //// Generic Xaml events
                    //logBuilder.XamlLogLevel(LogLevel.Debug);
                    //// Layout specific messages
                    //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                    //// Storage messages
                    //logBuilder.StorageLogLevel(LogLevel.Debug);
                    //// Binding related messages
                    //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                    //// Binder memory references tracking
                    //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                    //// DevServer and HotReload related
                    //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                    //// Debug JS interop
                    //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);

                }, enableUnoLogging: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<AppConfig>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                // Register Json serializers (ISerializer and ISerializer)
                .UseSerialization((context, services) => services
                    .AddContentSerializer(context)
                    .AddJsonTypeInfo(WeatherForecastContext.Default.IImmutableListWeatherForecast))
                .UseHttp((context, services) =>
                {

#if DEBUG
                    services.AddTransient<DelegatingHandler, DebugHttpHandler>();
#endif

#if !DEBUG

                    services.AddTransient<ITrendService, MockTrendService>()
                        .AddTransient<IContactService, MockContactService>()
                        .AddTransient<IUserService, MockUserService>()
                        .AddTransient<IMessageRecordService, MockMessageRecordService>();
#else
                    services.AddRefitClient<ITrendService>(context)
                        .AddRefitClient<IContactService>(context)
                        .AddRefitClient<IUserService>(context)
                        .AddRefitClient<IMessageRecordService>(context);
#endif

                    services.AddSingleton<IWeatherCache, WeatherCache>()
                        .AddRefitClient<IApiClient>(context);
                })
                //.UseAuthentication(auth =>
                //{
                //    auth.AddCustom(custom =>
                //    {
                //        custom.Login((sp, dispatcher, credentials, cancellationToken) =>
                //        {
                //            // TODO: Write code to process credentials that are passed into the LoginAsync method
                //            if (credentials?.TryGetValue(nameof(LoginViewModel.Username), out var username) ?? false &&
                //                !username.IsNullOrEmpty())
                //            {
                //                // Return IDictionary containing any tokens used by service calls or in the app
                //                credentials ??= new Dictionary<string, string>();
                //                credentials[TokenCacheExtensions.AccessTokenKey] = "SampleToken";
                //                credentials[TokenCacheExtensions.RefreshTokenKey] = "RefreshToken";
                //                credentials["Expiry"] = DateTime.Now.AddMinutes(5).ToString("g");
                //                return ValueTask.FromResult<IDictionary<string, string>?>(credentials);
                //            }

                //            // Return null/default to fail the LoginAsync method
                //            return ValueTask.FromResult<IDictionary<string, string>?>(default);
                //        }).Refresh((sp, tokenDictionary, cancellationToken) =>
                //        {
                //            if ((tokenDictionary?.TryGetValue(TokenCacheExtensions.RefreshTokenKey, out var refreshToken) ?? false) &&
                //                !refreshToken.IsNullOrEmpty() &&
                //                (tokenDictionary?.TryGetValue("Expiry", out var expiry) ?? false) &&
                //                DateTime.TryParse(expiry, out var tokenExpiry) &&
                //                tokenExpiry > DateTime.Now)
                //            {
                //                // Return IDictionary containing any tokens used by service calls or in the app
                //                tokenDictionary ??= new Dictionary<string, string>();
                //                tokenDictionary[TokenCacheExtensions.AccessTokenKey] = "NewSampleToken";
                //                tokenDictionary["Expiry"] = DateTime.Now.AddMinutes(5).ToString("g");
                //                return ValueTask.FromResult<IDictionary<string, string>?>(tokenDictionary);
                //            }

                //            // Return null/default to fail the Refresh method
                //            return ValueTask.FromResult<IDictionary<string, string>?>(default);
                //        });
                //    }, name: "CustomAuth");
                //})
                .ConfigureServices((context, services) =>
                {

#if DEBUG
                    // ApplicationData.Current 在未打包发布的情况下引起的问题， EncryptedApplicationDataKeyValueStorage
                    services.RemoveWhere(a => a.ServiceType.Equals(typeof(IKeyValueStorage)))
                        .RemoveWhere(a => a.ServiceType.Equals(typeof(INamedInstance<IKeyValueStorage>)));

                    services.AddNamedSingleton<IKeyValueStorage, InMemoryKeyValueStorage>(InMemoryKeyValueStorage.Name)
                        .SetDefaultInstance<IKeyValueStorage>(InMemoryKeyValueStorage.Name);

#endif

                })
                .UseNavigation(RegisterRoutes)
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.EnableHotReload();
#endif

        Host = await builder.NavigateAsync<Shell>(initialNavigate: async (services, navigator) =>
        {
//            var auth = services.GetRequiredService<IAuthenticationService>();

//            var authenticated = await auth.LoginAsync();

//#if DEBUG
//            authenticated = true;
//#endif
//            if (authenticated)
//            {
                await navigator.NavigateViewModelAsync<FullViewModel>(this, qualifier: Qualifiers.Nested);
            //}
            //else
            //{
            //    await navigator.NavigateViewModelAsync<GuideViewModel>(this, qualifier: Qualifiers.Nested);
            //}
        });
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {

        views.Register(new ViewMap[] {
            new ViewMap(ViewModel: typeof(ShellViewModel)),
            new ViewMap<Full,FullViewModel>(),
            new ViewMap<GuidePage, GuideViewModel>(),
            new ViewMap<MainPage, MainViewModel>(),
            new ViewMap<LoginPage, LoginViewModel>(),
            new ViewMap<SecondPage, SecondViewModel>(),
            new ViewMap<SecondPage, SecondViewModel>(),
            new ViewMap<SettingPage, SettingViewModel>(),
            new ViewMap<PrivacyPage, PrivacyViewModel>(),
            new DataViewMap<TrendPage, TrendViewModel, TrendDto>(),
            new DataViewMap<MessagePage, MessageViewModel, ContactDto>(),
            new DataViewMap<LikePage, LikeViewModel, SelectInt>(),
            new DataViewMap<UserPage, UserViewModel, UserDto>(),
        });

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<ShellViewModel>(),
                Nested: new RouteMap[]
                {
                    new RouteMap("Full", View: views.FindByViewModel<FullViewModel>()),
                    new RouteMap("Guide", View: views.FindByViewModel<GuideViewModel>()),
                    new RouteMap("Main", View: views.FindByViewModel<MainViewModel>()),
                    new RouteMap("Login", View: views.FindByViewModel<LoginViewModel>()),
                    new RouteMap("Second", View: views.FindByViewModel<SecondViewModel>()),
                    new RouteMap("Setting",View:views.FindByViewModel<SettingViewModel>()),
                    new RouteMap("Privacy",View:views.FindByViewModel<PrivacyViewModel>()),
                    new RouteMap("Trend",View:views.FindByViewModel<TrendViewModel>()),
                    new RouteMap("Message",View:views.FindByViewModel<MessageViewModel>()),
                    new RouteMap("Like",View:views.FindByViewModel<LikeViewModel>()),
                    new RouteMap("User",View:views.FindByViewModel<UserViewModel>()),
                }
            )
        );
    }
}
