using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class CreateProductPage: ContentPage
{
	public CreateProductPage()
	{
		InitializeComponent();
		BindingContext = new CreateProductPageViewModel(Navigation);
	}
}