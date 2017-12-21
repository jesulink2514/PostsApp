using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PostsApp.Models;
using PostsApp.Services;

namespace PostsApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IServiceFactory<IPostsService> _postsService;
        private readonly IUserDialogs _userDialogs;

        public MainPageViewModel(
            IServiceFactory<IPostsService> postsService,
            IUserDialogs userDialogs,
            INavigationService navigationService) 
            : base (navigationService)
        {
            _postsService = postsService;
            _userDialogs = userDialogs;
            RefreshPostsCommand = new DelegateCommand(async()=> await LoadPosts());
            ViewCommentsCommand= new DelegateCommand<Post>(ViewComments);
        }

        private async void ViewComments(Post post)
        {
            await NavigationService.NavigateAsync($"CommentsPage?postId={post.Id}");
        }

        public bool IsLoading { get; set; }
        public ICommand RefreshPostsCommand { get; private set; }
        public ICommand ViewCommentsCommand { get; private set; }
        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadPosts();
            await LoadPhotos();
        }

        private async Task LoadPosts()
        {
            IsLoading = true;

            Posts = (await _postsService.UserInitiated.ListPostsAsync()).ToList();

            _userDialogs.Toast("Posts updated");

            IsLoading = false;
        }

        private async Task LoadPhotos()
        {
            var tasks = Posts.Select(x => _postsService.Background.GetCommentsFromPost(x.Id))
                .ToList();

            await Task.WhenAll(tasks).ConfigureAwait(true);
        }


        public List<Post> Posts { get; set; }
    }
}
