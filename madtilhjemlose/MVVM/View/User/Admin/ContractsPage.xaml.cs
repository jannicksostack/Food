using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class ContractsPage : ContentPage
{
	public ContractsPage()
	{
		InitializeComponent();
		BindingContext = new ContractsPageViewModel(Navigation);
	}
}