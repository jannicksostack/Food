using madtilhjemlose.MVVM.ViewModel.User.Restricted;

namespace madtilhjemlose.MVVM.View.User.Restricted;

public partial class ContractPage : ContentPage
{
	public ContractPage()
	{
		InitializeComponent();
		BindingContext = new ContractPageViewModel(Navigation);
	}
}