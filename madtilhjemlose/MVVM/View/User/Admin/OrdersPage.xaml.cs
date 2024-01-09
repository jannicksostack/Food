using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class OrdersPage : ContentPage
{
	public OrdersPage()
	{
		InitializeComponent();
		BindingContext = new OrdersPageViewModel(Navigation);
	}
}