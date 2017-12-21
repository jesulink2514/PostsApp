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
	    public void OnNavigatedFrom(NavigationParameters parameters)
	    {
	    }

	    public void OnNavigatedTo(NavigationParameters parameters)
	    {
	        //Descargar fotos;
	    }

	    public List<Photo> Photos { get; private set; }
	}
}
