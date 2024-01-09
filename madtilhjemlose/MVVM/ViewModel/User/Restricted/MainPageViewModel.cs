using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.User.Restricted;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Restricted
{
    internal class MainPageViewModel : ObservableValidator
    {
        private INavigation navigation;
        public ICommand LogoutCommand { get; set; }
        public ICommand ViewContractCommand { get; set; }
        public ICommand ViewLastOrderCommand { get; set; }
        public ICommand NewOrderCommand { get; set; }


        public MainPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            LogoutCommand = new RelayCommand(Logout);
            ViewContractCommand = new RelayCommand(ViewContract);
            ViewLastOrderCommand = new RelayCommand(ViewLastOrder);
            NewOrderCommand = new RelayCommand(NewOrder);
        }

        private void NewOrder()
        {
            navigation.PushAsync(new NewOrderPage());
        }

        private void ViewLastOrder()
        {
            navigation.PushAsync(new LastOrderPage());
        }

        private void ViewContract()
        {
            navigation.PushAsync(new ContractPage());
        }

        private void Logout()
        {
            navigation.PopAsync();
        }
    }
}
