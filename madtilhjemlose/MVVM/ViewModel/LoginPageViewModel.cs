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
            Model.User.User? maybeUser = userRepo.TryGetUser(Username, Password);
            if (maybeUser is not Model.User.User user)
            {
                Clear();
                await OnFailedLogin();
                return;
            }

            Page p = user.UserType switch
            {
                UserType.Admin => new View.User.Admin.MainPage(),
                UserType.Default => new View.User.Default.MainPage(),
                UserType.Restricted => new View.User.Restricted.MainPage(),
                _ => throw new InvalidDataException("Der findes kun 3 bruger typer")
            };

            Model.User.User.Current = user;

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
