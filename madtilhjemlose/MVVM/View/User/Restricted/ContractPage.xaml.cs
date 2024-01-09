using madtilhjemlose.MVVM.ViewModel.Customer;

namespace madtilhjemlose.MVVM.View.Customer;

public partial class ContractPage : ContentPage
{
	public ContractPage()
	{
		InitializeComponent();
		BindingContext = new ContractPageViewModel(Navigation);
	}
}