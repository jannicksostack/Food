using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class CreateContractPage : ContentPage
{
	public CreateContractPage()
	{
		InitializeComponent();
		BindingContext = new CreateContractPageViewModel(Navigation);
	}
}