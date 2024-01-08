using madtilhjemlose.MVVM.ViewModel;

namespace madtilhjemlose.MVVM.View;

public partial class LoginPage : ContentPage
{


	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginPageViewModel(Navigation, OnError);
	}

	private async Task OnError()
	{
		await DisplayAlert("Failed to login", "Wrong username or password", "Ok");
	}
}