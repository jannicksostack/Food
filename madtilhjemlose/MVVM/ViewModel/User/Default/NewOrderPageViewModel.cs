using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Default
{
    internal class NewOrderPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public NewOrderPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

    }
}
