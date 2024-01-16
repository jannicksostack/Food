using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.ViewModel.User.Admin;

namespace madtilhjemlose.MVVM.View.User.Admin;

public partial class CompanyPage : ContentPage
{
	public CompanyPage()
	{
		InitializeComponent();
        BindingContext = new CompanyPageViewModel();

    }

    async void Button_Clicked(object sender, EventArgs e)
    {

    }

    async void LogOffButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}