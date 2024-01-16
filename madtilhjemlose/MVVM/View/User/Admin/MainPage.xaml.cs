using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}
}