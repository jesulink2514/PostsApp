using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;

namespace PostsApp.ViewModels
{
    public class MenuMasterPageViewModel : BindableBase, INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        public MenuMasterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            this.GoToCommand = new DelegateCommand<MenuItem>(GoTo);

            this.MenuOptions = new List<MenuItem>()
            {
                new MenuItem("Posts","NavigationPage/MainPage",GoToCommand),
                new MenuItem("Photos", "NavigationPage/PhotosPage",GoToCommand),
            };
        }

        public List<MenuItem> MenuOptions { get; set; }

        private void GoTo(MenuItem selected)
        {
            SelectedMenu = selected;
            IsPresented = false;
            _navigationService.NavigateAsync(selected.Path);
        }

        public bool IsPresented { get; set; }
        public MenuItem SelectedMenu { get; set; }
        public DelegateCommand<MenuItem> GoToCommand { get; private set; }
    }

    public class MenuItem : BindableBase
{
    public MenuItem(
        string text, 
        string path, 
        ICommand goToCommand)
    {
        Text = text;
        Path = path;
        GoToCommand = goToCommand;
    }

    public string Text { get; set; }
    public string Path { get; set; }
    public ICommand GoToCommand { get; private set; }
}
}
