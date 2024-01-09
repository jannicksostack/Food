using madtilhjemlose.MVVM.ViewModel.User.Default;

namespace madtilhjemlose.MVVM.View.User.Default;

public partial class NewOrderPage : ContentPage
{
	public NewOrderPage()
	{
		InitializeComponent();
		BindingContext = new NewOrderPageViewModel(Navigation);
	}
}