using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.ViewModel.User.Default;

namespace madtilhjemlose.MVVM.View.User.Default;

public partial class ShoppingCartPage : ContentPage
{
	public ShoppingCartPage(ShoppingCart cart)
	{
		InitializeComponent();
		BindingContext = new ShoppingCartViewModel(cart);
	}
}