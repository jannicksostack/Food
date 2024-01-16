using madtilhjemlose.MVVM.ViewModel.User.Default;

namespace madtilhjemlose.MVVM.View.User.Default;

public partial class LastOrderPage : ContentPage
{
	public LastOrderPage(int userId)
	{
		InitializeComponent();
		BindingContext = new LastOrderPageViewModel(userId);
	}
}