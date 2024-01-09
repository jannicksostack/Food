using madtilhjemlose.MVVM.ViewModel.User.Restricted;

namespace madtilhjemlose.MVVM.View.User.Restricted;

public partial class NewOrderPage : ContentPage
{
	public NewOrderPage()
	{
		InitializeComponent();
		BindingContext = new NewOrderPageViewModel(Navigation);
	}
}