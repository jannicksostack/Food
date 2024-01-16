using madtilhjemlose.MVVM.Model.User;
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
            switch (User.Current.UserType)
            {
                case UserType.Admin:
                    App.PopToPage<MVVM.View.User.Admin.MainPage>();
                    break;
                case UserType.Default:
                    App.PopToPage<MVVM.View.User.Default.MainPage>();
                    break;
                case UserType.Restricted:
                    App.PopToPage<MVVM.View.User.Restricted.MainPage>();
                    break;
            }
        }
        public static void PopToPage<T>() where T: Page
        {
            List<Page> stack = App.Navigation.NavigationStack.ToList();
            int index = stack.FindLastIndex(x => x.GetType() == typeof(T));

            if (index == -1)
            {
                throw new InvalidDataException("Page \"" + typeof(T) + "\" not found in NavigationStack");
            }

            var pagesToRemove = stack
                .Skip(index + 1)
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
