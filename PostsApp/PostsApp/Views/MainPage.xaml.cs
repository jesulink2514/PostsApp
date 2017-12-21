using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PostsApp.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

	    private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var item = e.SelectedItem;
	        var command = (this.BindingContext as dynamic).ViewCommentsCommand as ICommand;
            command?.Execute(item); 
	    }
	}
}