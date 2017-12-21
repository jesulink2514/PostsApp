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
	public class PhotosPageViewModel : BindableBase, INavigatedAware
	{
	    private readonly IServiceFactory<IPostsService> _postsService;

	    public PhotosPageViewModel(IServiceFactory<IPostsService> postsService)
	    {
	        _postsService = postsService;
	    }

	    public void OnNavigatedFrom(NavigationParameters parameters)
	    {
	    }

	    public async void OnNavigatedTo(NavigationParameters parameters)
	    {
	        Photos = App.Photos ?? 
                (await _postsService.UserInitiated.ListPhotosAsync()).ToList();
	    }

	    public List<Photo> Photos { get; private set; }
	}
}
