using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.User.Restricted;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Restricted
{
    internal class MainPageViewModel : ObservableValidator
    {
        public ICommand LogoutCommand { get; set; }
        public ICommand ViewContractCommand { get; set; }
        public ICommand ViewLastOrderCommand { get; set; }


        public MainPageViewModel()
        {
            LogoutCommand = new RelayCommand(Logout);
            ViewContractCommand = new RelayCommand(ViewContract);
            ViewLastOrderCommand = new RelayCommand(ViewLastOrder);
        }

        private void ViewLastOrder()
        {
            App.Navigation.PushAsync(new LastOrderPage());
        }

        private void ViewContract()
        {
            App.Navigation.PushAsync(new ContractPage());
        }

        private void Logout()
        {
            App.Navigation.PopToRootAsync();
        }
    }
}
