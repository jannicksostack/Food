using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.ViewModel.User.Restricted;

namespace madtilhjemlose.MVVM.View.User.Restricted;

public partial class MainPage : ContentPage
{
	public MainPage(DefaultUser user)
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel(Navigation);
	}
}