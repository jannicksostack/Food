using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class MainPageViewModel : ObservableValidator
    {
        public AdminUser User { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand OpenContractsCommand { get; set; }
        public ICommand OpenOrdersCommand { get; set; }
        public ICommand OpenProductsCommand { get; set; }
        public ICommand OpenStatisticsCommand { get; set; }
        public ICommand OpenCompanyCommand { get; set; }
        public ICommand OpenActiveProductsCommand { get; set; }


        public MainPageViewModel(AdminUser user)
        {
            User = user;
            LogoutCommand = new RelayCommand(logout);
            OpenCompanyCommand = new RelayCommand(OpenCompany);
            OpenContractsCommand = new RelayCommand(OpenContracts);
            OpenOrdersCommand = new RelayCommand(OpenOrders);
            OpenProductsCommand = new RelayCommand(OpenProducts);
            OpenStatisticsCommand = new RelayCommand(OpenStatistics);
            OpenActiveProductsCommand = new RelayCommand(OpenActiveProducts);
        }

        private void OpenActiveProducts()
        {
            App.Current.MainPage.Navigation.PushAsync(new ActiveProductsPage());
        }

        private void OpenStatistics()
        {
            App.Current.MainPage.Navigation.PushAsync(new StatisticsPage());
        }

        private void OpenProducts()
        {
            App.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }

        private void OpenOrders()
        {
            App.Current.MainPage.Navigation.PushAsync(new OrdersPage());
        }

        private void OpenContracts()
        {
            App.Current.MainPage.Navigation.PushAsync(new ContractsPage());
        }

        private void OpenCompany()
        {
            App.Current.MainPage.Navigation.PushAsync(new CompanyPage());
        }

        private void logout()
        {
            App.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
