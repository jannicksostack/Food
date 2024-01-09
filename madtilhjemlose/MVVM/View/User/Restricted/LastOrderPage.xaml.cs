using madtilhjemlose.MVVM.ViewModel.User.Restricted;

namespace madtilhjemlose.MVVM.View.User.Restricted;

public partial class LastOrderPage : ContentPage
{
	public LastOrderPage()
	{
		InitializeComponent();
		BindingContext = new LastOrderPageViewModel(Navigation);
	}
}