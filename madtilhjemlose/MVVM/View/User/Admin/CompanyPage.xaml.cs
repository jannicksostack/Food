using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class CompanyPage : ContentPage
{
	public CompanyPage()
	{
		InitializeComponent();
        BindingContext = new CompanyPageViewModel(Navigation);

    }
}