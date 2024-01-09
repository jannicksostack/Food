using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Restricted
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
