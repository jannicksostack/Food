using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class OrdersPage : ContentPage
{
	public OrdersPage()
	{
		InitializeComponent();
		BindingContext = new OrdersPageViewModel(Navigation);
	}
}