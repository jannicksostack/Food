using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Default
{
    internal class ContractPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public ContractPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}
