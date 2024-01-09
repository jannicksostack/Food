using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model.User;
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
                DefaultUser defaultUser => new View.User.Default.MainPage(defaultUser),
                RestrictedUser restrictedUser => new View.User.Restricted.MainPage(restrictedUser),
                _ => throw new InvalidDataException("Der findes kun 3 bruger typer")
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
