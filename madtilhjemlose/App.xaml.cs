using madtilhjemlose.MVVM.View;

namespace madtilhjemlose
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
