using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class ProductsPage : ContentPage
{
	public ProductsPage()
	{
		InitializeComponent();
		BindingContext = new ProductsPageViewModel(Navigation);
	}
}