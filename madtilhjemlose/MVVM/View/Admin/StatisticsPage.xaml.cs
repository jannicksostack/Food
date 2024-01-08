using madtilhjemlose.MVVM.ViewModel.Admin;

namespace madtilhjemlose.MVVM.View.Admin;

public partial class StatisticsPage: ContentPage
{
	public StatisticsPage()
	{
		InitializeComponent();
		BindingContext = new StatisticsPageViewModel(Navigation);
	}
}