using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.Admin
{
    internal class StatisticsPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public StatisticsPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}
