using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using PostsApp.Models;
using PostsApp.Services;
using Prism.Navigation;

namespace PostsApp.ViewModels
{
	public class CommentsPageViewModel : BindableBase, INavigatedAware

    {
        private readonly IServiceFactory<IPostsService> _postService;

        public CommentsPageViewModel(IServiceFactory<IPostsService> postService)
        {
            _postService = postService;
        }

        public List<Comment> Comments { get;private set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var postId = parameters.GetValue<int>("postId");

            var comments = await _postService.UserInitiated.GetCommentsFromPost(postId);

            Comments = comments.ToList();
        }
    }
}
