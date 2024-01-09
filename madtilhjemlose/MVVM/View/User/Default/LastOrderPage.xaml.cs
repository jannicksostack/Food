using madtilhjemlose.MVVM.ViewModel.User.Default;

namespace madtilhjemlose.MVVM.View.User.Default;

public partial class LastOrderPage : ContentPage
{
	public LastOrderPage()
	{
		InitializeComponent();
		BindingContext = new LastOrderPageViewModel(Navigation);
	}
}