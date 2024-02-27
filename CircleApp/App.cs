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

                    services.AddTransient<ITrendService, MockTrendService>()
                        .AddTransient<IContactService, MockContactService>()
                        .AddTransient<IUserService, MockUserService>()
                        .AddTransient<IMessageRecordService, MockMessageRecordService>();

#if !DEBUG

                    //services.AddRefitClient<ITrendService>(context)
                    //    .AddRefitClient<IContactService>(context)
                    //    .AddRefitClient<IUserService>(context)
                    //    .AddRefitClient<IMessageRecordService>(context);
#endif

                    services.AddSingleton<IWeatherCache, WeatherCache>()
                        .AddRefitClient<IApiClient>(context);
                })
                .UseAuthentication(auth =>
                {
                    auth.AddCustom(custom =>
                    {
                        custom.Login((sp, dispatcher, credentials, cancellationToken) =>
                        {
                            // TODO: Write code to process credentials that are passed into the LoginAsync method
                            if (credentials?.TryGetValue(nameof(LoginModel.Username), out var username) ?? false &&
                                !username.IsNullOrEmpty())
                            {
                                // Return IDictionary containing any tokens used by service calls or in the app
                                credentials ??= new Dictionary<string, string>();
                                credentials[TokenCacheExtensions.AccessTokenKey] = "SampleToken";
                                credentials[TokenCacheExtensions.RefreshTokenKey] = "RefreshToken";
                                credentials["Expiry"] = DateTime.Now.AddMinutes(5).ToString("g");
                                return ValueTask.FromResult<IDictionary<string, string>?>(credentials);
                            }

                            // Return null/default to fail the LoginAsync method
                            return ValueTask.FromResult<IDictionary<string, string>?>(default);
                        }).Refresh((sp, tokenDictionary, cancellationToken) =>
                        {
                            if ((tokenDictionary?.TryGetValue(TokenCacheExtensions.RefreshTokenKey, out var refreshToken) ?? false) &&
                                !refreshToken.IsNullOrEmpty() &&
                                (tokenDictionary?.TryGetValue("Expiry", out var expiry) ?? false) &&
                                DateTime.TryParse(expiry, out var tokenExpiry) &&
                                tokenExpiry > DateTime.Now)
                            {
                                // Return IDictionary containing any tokens used by service calls or in the app
                                tokenDictionary ??= new Dictionary<string, string>();
                                tokenDictionary[TokenCacheExtensions.AccessTokenKey] = "NewSampleToken";
                                tokenDictionary["Expiry"] = DateTime.Now.AddMinutes(5).ToString("g");
                                return ValueTask.FromResult<IDictionary<string, string>?>(tokenDictionary);
                            }

                            // Return null/default to fail the Refresh method
                            return ValueTask.FromResult<IDictionary<string, string>?>(default);
                        });
                    }, name: "CustomAuth");
                })
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
                .UseNavigation(ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes)
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.EnableHotReload();
#endif

        Host = await builder.NavigateAsync<Shell>(initialNavigate: async (services, navigator) =>
        {
            var auth = services.GetRequiredService<IAuthenticationService>();

            var authenticated = await auth.LoginAsync();

#if DEBUG
            authenticated = true;
#endif
            if (authenticated)
            {
                await navigator.NavigateViewModelAsync<FullModel>(this, qualifier: Qualifiers.Nested);
            }
            else
            {
                await navigator.NavigateViewModelAsync<GuideModel>(this, qualifier: Qualifiers.Nested);
            }
        });
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {

        views.Register(new ViewMap[] {
            new ViewMap(ViewModel: typeof(ShellModel)),
            new ViewMap<Full,FullModel>(),
            new ViewMap<GuidePage, GuideModel>(),
            new ViewMap<MainPage, MainModel>(),
            new ViewMap<LoginPage, LoginModel>(),
            new ViewMap<SecondPage, SecondModel>(),
            new ViewMap<SecondPage, SecondModel>(),
            new ViewMap<SettingPage, SettingModel>(),
            new ViewMap<PrivacyPage, PrivacyModel>(),
            new DataViewMap<TrendPage, TrendModel, TrendDto>(),
            new DataViewMap<MessagePage, MessageModel, ContactDto>(),
            new DataViewMap<LikePage, LikeModel, SelectInt>(),
            new DataViewMap<UserPage, UserModel, UserDto>(),
        });

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<ShellModel>(),
                Nested: new RouteMap[]
                {
                    new RouteMap("Full", View: views.FindByViewModel<FullModel>()),
                    new RouteMap("Guide", View: views.FindByViewModel<GuideModel>()),
                    new RouteMap("Main", View: views.FindByViewModel<MainModel>()),
                    new RouteMap("Login", View: views.FindByViewModel<LoginModel>()),
                    new RouteMap("Second", View: views.FindByViewModel<SecondModel>()),
                    new RouteMap("Setting",View:views.FindByViewModel<SettingModel>()),
                    new RouteMap("Privacy",View:views.FindByViewModel<PrivacyModel>()),
                    new RouteMap("Trend",View:views.FindByViewModel<TrendModel>()),
                    new RouteMap("Message",View:views.FindByViewModel<MessageModel>()),
                    new RouteMap("Like",View:views.FindByViewModel<LikeModel>()),
                    new RouteMap("User",View:views.FindByViewModel<UserModel>()),
                }
            )
        );
    }
}
