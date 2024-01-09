using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminUser admin)
	{
		InitializeComponent();
		BindingContext = new AdminPageViewModel(Navigation);
	}
}