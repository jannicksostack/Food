using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class UpdateProductPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public UpdateProductPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}
