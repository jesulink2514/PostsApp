using Prism.Mvvm;
using System.Collections.Generic;
using PostsApp.Models;
using Prism.Navigation;

namespace PostsApp.ViewModels
{
	public class CommentsPageViewModel : BindableBase, INavigatedAware
    {
        public List<Comment> Comments { get;private set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var postId = parameters.GetValue<int>("postId");
            //Descargar commentarios para el Post seleccionado
        }
    }
}
