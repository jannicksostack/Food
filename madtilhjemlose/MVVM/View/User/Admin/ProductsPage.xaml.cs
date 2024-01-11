using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class ProductsPage : ContentPage
{
	public ProductsPage()
	{
		InitializeComponent();
		BindingContext = new ProductsPageViewModel();
	}
}