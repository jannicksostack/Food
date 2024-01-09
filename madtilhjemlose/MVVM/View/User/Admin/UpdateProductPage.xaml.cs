using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class UpdateProductPage: ContentPage
{
	public UpdateProductPage()
	{
		InitializeComponent();
		BindingContext = new UpdateProductPageViewModel(Navigation);
	}
}