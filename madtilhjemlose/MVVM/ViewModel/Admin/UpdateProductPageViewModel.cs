using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.Admin
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
