using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PostsApp.Views;
using Autofac;
using PostsApp.Models;
using PostsApp.Services;
using Prism.Autofac;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PostsApp
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }
        public static List<Photo> Photos { get; set; }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MenuMasterPage/NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Builder.RegisterTypeForNavigation<NavigationPage>();
            Builder.RegisterTypeForNavigation<MainPage>();
            Builder.RegisterTypeForNavigation<MenuMasterPage>();
            Builder.RegisterTypeForNavigation<PhotosPage>();
            Builder.RegisterTypeForNavigation<CommentsPage>();

            Locator.CurrentMutable.RegisterConstant(new TraceHandler(), typeof(HttpMessageHandler));

            Builder.Register((c) => new ServiceFactory<IPostsService>("https://jsonplaceholder.typicode.com"))
                .AsImplementedInterfaces()
                .SingleInstance();

            Builder.RegisterInstance(UserDialogs.Instance);

            //NetCache.RequestCache = new InMemoryCache();
        }
    }

    public class TraceHandler : DelegatingHandler
    {
        private readonly Stopwatch _stopWatch;

        public TraceHandler() : base(new HttpClientHandler())
        {
            _stopWatch = new Stopwatch();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Debug.WriteLine($"{request.Method} - {request.RequestUri}");
            var resp = await base.SendAsync(request, cancellationToken);
            _stopWatch.Stop();
            Debug.WriteLine($"{resp.StatusCode} - {_stopWatch.ElapsedMilliseconds}ms ellapsed");
            Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            
            return resp;

        }
    }
}
