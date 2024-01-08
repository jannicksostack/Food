using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class ContractsPage : ContentPage
{
	public ContractsPage()
	{
		InitializeComponent();
		BindingContext = new ContractsPageViewModel(Navigation);
	}
}