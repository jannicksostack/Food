using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class CreateContractPage : ContentPage
{
	public CreateContractPage()
	{
		InitializeComponent();
		BindingContext = new CreateContractPageViewModel(Navigation);
	}
}