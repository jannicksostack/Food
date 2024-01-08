using madtilhjemlose.MVVM.ViewModel;

namespace madtilhjemlose.MVVM.View;

public partial class LoginPage : ContentPage
{


	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginPageViewModel(Navigation);
	}
}