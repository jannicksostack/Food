using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;


namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{ 
    internal class CompanyPageViewModel : ObservableValidator
    {

        private INavigation navigation;

        public ICommand SearchCommand { get; set; }
   
        public ICommand UpdateCommand { get; set; }
        public ICommand CreateCommand { get; set; }


        public CompanyPageViewModel(INavigation navigation)
        { 
        this.navigation = navigation;
        SearchCommand = new RelayCommand(Search);
        UpdateCommand = new RelayCommand(Update);
        CreateCommand = new RelayCommand(Create);

        }

   
        private void Search()
        {

        }

        private void Update() 
        {
        //navigation.PushAsync(new CreateCompanyPage());

        }

        private void Create()
        {

        }

        //public ICommand PerformSearch => new Command<string>((string query) =>
        //{
        //    SearchResults = DataService.GetSearchResults(query);

        //});
        //private List<string> SearchResults = DataService.Firma;
        //public List<string> SearchResults
        //{  get
        //    { return SearchResults; }
        //   set
        //    { SearchResults = value; }
    }
}