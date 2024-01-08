using madtilhjemlose.MVVM.ViewModel.Customer;

namespace madtilhjemlose.MVVM.View.Customer;

public partial class CustomerPage : ContentPage
{
	public CustomerPage()
	{
		InitializeComponent();
		BindingContext = new CustomerPageViewModel(Navigation);
	}
}