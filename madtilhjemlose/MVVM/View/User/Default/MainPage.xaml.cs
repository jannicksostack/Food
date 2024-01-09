using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.ViewModel.User.Default;

namespace madtilhjemlose.MVVM.View.User.Default;

public partial class MainPage : ContentPage
{
	public MainPage(DefaultUser user)
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel(Navigation);
	}
}