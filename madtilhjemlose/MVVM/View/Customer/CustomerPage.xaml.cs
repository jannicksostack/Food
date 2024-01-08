using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.ViewModel.Customer;

namespace madtilhjemlose.MVVM.View.Customer;

public partial class CustomerPage : ContentPage
{
	public CustomerPage(DefaultUser user)
	{
		InitializeComponent();
		BindingContext = new CustomerPageViewModel(Navigation);
	}
}