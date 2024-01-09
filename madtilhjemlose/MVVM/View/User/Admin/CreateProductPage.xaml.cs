using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class CreateProductPage: ContentPage
{
	public CreateProductPage()
	{
		InitializeComponent();
		BindingContext = new CreateProductPageViewModel(Navigation);
	}
}