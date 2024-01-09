using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class UpdateProductPage: ContentPage
{
	public UpdateProductPage()
	{
		InitializeComponent();
		BindingContext = new UpdateProductPageViewModel(Navigation);
	}
}