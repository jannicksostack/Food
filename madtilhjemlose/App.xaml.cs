using madtilhjemlose.MVVM.View;

namespace madtilhjemlose
{
    public partial class App : Application
    {
        public static INavigation Navigation => App.Current.MainPage.Navigation;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
