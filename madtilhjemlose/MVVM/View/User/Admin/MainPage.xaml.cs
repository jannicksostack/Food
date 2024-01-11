using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class MainPage : ContentPage
{
	public MainPage(AdminUser admin)
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel(admin);
	}
}