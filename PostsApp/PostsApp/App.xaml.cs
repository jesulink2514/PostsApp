using System.Collections.Generic;
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

            Builder.RegisterInstance(UserDialogs.Instance);
        }
    }
}
