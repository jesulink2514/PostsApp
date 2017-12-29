using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Fusillade;
using PostsApp.Models;
using PostsApp.Services;

namespace PostsApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IApi<IPostsService> _postsService;

        public MainPageViewModel(
            IUserDialogs userDialogs,
            INavigationService navigationService,
            IApi<IPostsService> postsService) 
            : base (navigationService)
        {
            _userDialogs = userDialogs;
            _postsService = postsService;
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
            NetCache.Speculative.ResetLimit(5 * 1024);

            await LoadPosts();

            await LoadComments();
        }   

        private async Task LoadPosts()
        {
            IsLoading = true;

            var posts = await _postsService.UserInitiated.ListPostsAsync();

            Posts = posts.ToList();

            _userDialogs.Toast("Posts updated");

            IsLoading = false;
        }

        private async Task LoadComments()
        {
            //Descargar 
            var tasks = Posts
                .Select(p => _postsService.Speculative.GetCommentsFromPost(p.Id)).ToArray();

            await Task.WhenAll(tasks);
        }


        public List<Post> Posts { get; set; }
    }
}
