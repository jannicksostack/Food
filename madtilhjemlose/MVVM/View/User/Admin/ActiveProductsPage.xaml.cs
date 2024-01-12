using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class ActiveProductsPage : ContentPage
{
	public ActiveProductsPage()
	{
		InitializeComponent();
		BindingContext = new ActiveProductsPageViewModel();
	}
}