using madtilhjemlose.MVVM.ViewModel.Customer;

namespace madtilhjemlose.MVVM.View.Customer;

public partial class LastOrderPage : ContentPage
{
	public LastOrderPage()
	{
		InitializeComponent();
		BindingContext = new LastOrderPageViewModel(Navigation);
	}
}