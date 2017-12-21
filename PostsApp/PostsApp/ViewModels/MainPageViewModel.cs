using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PostsApp.Models;

namespace PostsApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IUserDialogs _userDialogs;

        public MainPageViewModel(
            IUserDialogs userDialogs,
            INavigationService navigationService) 
            : base (navigationService)
        {
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
        }

        private async Task LoadPosts()
        {
            IsLoading = true;

            //Descargar posts

            _userDialogs.Toast("Posts updated");

            IsLoading = false;
        }

        private async Task LoadComments()
        {
            //Descargar 
        }


        public List<Post> Posts { get; set; }
    }
}
