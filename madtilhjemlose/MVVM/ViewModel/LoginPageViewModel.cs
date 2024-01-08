using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.Admin;
using madtilhjemlose.MVVM.View.Customer;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel
{
    internal partial class LoginPageViewModel : ObservableValidator
    {
        private INavigation navigation;
        public ICommand LoginCommand { get; set; }

        [ObservableProperty]
        private string username = "";
        [ObservableProperty]
        private string password = "";


        public LoginPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            Page p;
            if (Username.ToLower() == "admin")
            {
                p = new AdminPage();
            } else
            {
                p = new CustomerPage();
            }
            navigation.PushAsync(p);
            Username = "";
            Password = "";
        }
    }
}
