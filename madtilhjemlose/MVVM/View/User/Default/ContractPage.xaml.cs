using madtilhjemlose.MVVM.ViewModel.User.Default;

namespace madtilhjemlose.MVVM.View.User.Default;

public partial class ContractPage : ContentPage
{
	public ContractPage()
	{
		InitializeComponent();
		BindingContext = new ContractPageViewModel(Navigation);
	}
}