using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
		BindingContext = new AdminPageViewModel(Navigation);
	}
}