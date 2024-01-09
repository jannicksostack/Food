using madtilhjemlose.MVVM.ViewModel.Customer;

namespace madtilhjemlose.MVVM.View.Customer;

public partial class NewOrderPage : ContentPage
{
	public NewOrderPage()
	{
		InitializeComponent();
		BindingContext = new NewOrderPageViewModel(Navigation);
	}
}