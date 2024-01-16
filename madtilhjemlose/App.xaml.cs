using madtilhjemlose.MVVM.View;
using PdfSharp.Fonts;

namespace madtilhjemlose
{
    public partial class App : Application
    {
        public static INavigation Navigation => App.Current.MainPage.Navigation;
        public static Page CurrentPage => App.Navigation.NavigationStack.Last();
        public App()
        {
            InitializeComponent();
            GlobalFontSettings.FontResolver = new FontResolver();
            MainPage = new NavigationPage(new LoginPage());
        }

        public static void PopToUserStartPage()
        {
            var pagesToRemove = App.Navigation.NavigationStack
                .Skip(2)
                .SkipLast(1)
                .ToList();

            foreach (Page page in pagesToRemove)
            {
                App.Navigation.RemovePage(page);
            }

            App.Navigation.PopAsync();
        }
    }
}
