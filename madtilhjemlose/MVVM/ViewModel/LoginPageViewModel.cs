using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.View.User.Restricted;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel
{
    // Test comment
    internal partial class LoginPageViewModel : ObservableValidator
    {
        private INavigation navigation;
        public ICommand LoginCommand { get; set; }

        private UserRepository userRepo = new();

        [ObservableProperty]
        private string username = "";
        [ObservableProperty]
        private string password = "";

        private Func<Task> OnFailedLogin { get; set; }

        public LoginPageViewModel(INavigation navigation, Func<Task> onFailedLogin)
        {
            OnFailedLogin = onFailedLogin;
            this.navigation = navigation;
            LoginCommand = new AsyncRelayCommand(Login);
        }

        private async Task Login()
        {
            BaseUser? maybeUser = userRepo.TryGetUser(Username, Password);
            if (maybeUser is not BaseUser user)
            {
                Clear();
                await OnFailedLogin();
                return;
            }

            Page p = user switch
            {
                AdminUser adminUser => new View.User.Admin.MainPage(adminUser),
                DefaultUser defaultUser => new View.User.Restricted.MainPage(defaultUser),
                _ => throw new InvalidDataException("TODO")
            };

            Clear();
            await navigation.PushAsync(p);
        }

        private void Clear()
        {
            Username = "";
            Password = "";
        }

    }
}
