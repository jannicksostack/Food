using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model.User;
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
            int companyId = Model.User.User.Current.CompanyId;
            int userId = Model.User.User.Current.Id;
            App.Navigation.PushAsync(new View.User.Default.LastOrderPage(userId));
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
