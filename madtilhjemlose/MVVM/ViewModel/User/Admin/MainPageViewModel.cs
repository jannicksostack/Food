using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class MainPageViewModel : ObservableValidator
    {
        public ICommand LogoutCommand { get; set; }
        public ICommand OpenContractsCommand { get; set; }
        public ICommand OpenOrdersCommand { get; set; }
        public ICommand OpenProductsCommand { get; set; }
        public ICommand OpenStatisticsCommand { get; set; }
        public ICommand OpenCompanyCommand { get; set; }
        public ICommand OpenActiveProductsCommand { get; set; }


        public MainPageViewModel()
        {
            LogoutCommand = new RelayCommand(Logout);
            OpenCompanyCommand = new RelayCommand(OpenCompany);
            OpenContractsCommand = new RelayCommand(OpenContracts);
            OpenOrdersCommand = new RelayCommand(OpenOrders);
            OpenProductsCommand = new RelayCommand(OpenProducts);
            OpenStatisticsCommand = new RelayCommand(OpenStatistics);
            OpenActiveProductsCommand = new RelayCommand(OpenActiveProducts);
        }

        private void OpenActiveProducts()
        {
            App.Navigation.PushAsync(new ActiveProductsPage());
        }

        private void OpenStatistics()
        {
            App.Navigation.PushAsync(new StatisticsPage());
        }

        private void OpenProducts()
        {
            App.Navigation.PushAsync(new ProductsPage());
        }

        private void OpenOrders()
        {
            App.Navigation.PushAsync(new OrdersPage());
        }

        private void OpenContracts()
        {
            App.Navigation.PushAsync(new ContractsPage());
        }

        private void OpenCompany()
        {
            App.Navigation.PushAsync(new CompanyPage());
        }

        private void Logout()
        {
            App.Navigation.PopToRootAsync();
        }
    }
}
