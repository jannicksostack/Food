using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class MainPageViewModel : ObservableValidator
    {
        private INavigation navigation;
        public ICommand LogoutCommand { get; set; }
        public ICommand OpenContractsCommand { get; set; }
        public ICommand OpenOrdersCommand { get; set; }
        public ICommand OpenProductsCommand { get; set; }
        public ICommand OpenStatisticsCommand { get; set; }


        public MainPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            LogoutCommand = new RelayCommand(logout);
            OpenContractsCommand = new RelayCommand(OpenContracts);
            OpenOrdersCommand = new RelayCommand(OpenOrders);
            OpenProductsCommand = new RelayCommand(OpenProducts);
            OpenStatisticsCommand = new RelayCommand(OpenStatistics);
        }

        private void OpenStatistics()
        {
            navigation.PushAsync(new StatisticsPage());
        }

        private void OpenProducts()
        {
            navigation.PushAsync(new ProductsPage());
        }

        private void OpenOrders()
        {
            navigation.PushAsync(new OrdersPage());
        }

        private void OpenContracts()
        {
            navigation.PushAsync(new ContractsPage());
        }

        private void logout()
        {
            navigation.PopAsync();
        }
    }
}
