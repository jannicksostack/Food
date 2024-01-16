using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.View.User.Admin;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{ 

    internal partial class CompanyPageViewModel : ObservableValidator
    {
        [ObservableProperty]
        private string name = "";
        [ObservableProperty]
        private string address = "";
        [ObservableProperty]
        private Company? selectedCompany;
        [ObservableProperty]
        private ObservableCollection<Company> items;
        [ObservableProperty]
        private ObservableCollection<Company> searchItems;
       
        private INavigation navigation;
        protected static CompanyRepository repository = [];
        private Company _company;

        

        partial void OnSelectedCompanyChanged(Company? value)
        {
            if (value is not null)
            {
                Name = value.Name;
                Address = value.Address;
            }
            UpdateCommand.NotifyCanExecuteChanged();

        }
        partial void OnNameChanged(string value)
        {
            CreateCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        
        }
        partial void OnAddressChanged(string value)
        {
            CreateCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }
        partial void OnItemsChanged(ObservableCollection<Company> value) { SearchItems = new(value); }

        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }

        private CompanyRepository repo = new();
        public CompanyPageViewModel()
        {
            CreateCommand = new RelayCommand(CreateCompany);
         
            repo.RepositoryChanged += (_, list) =>
            {
                Items = new(list);
            };

            repo.GetCompanies();

            SearchCommand = new RelayCommand<string>(Search);
            UpdateCommand = new RelayCommand(Update);
            CreateCommand = new RelayCommand(CreateCompany);
        }


        private void OnCompaniesChanged(object? sender, ObservableCollection<Company>list)
        {
            Items = list;

        }

        private void Search (string query)
        {
            SearchItems = new(Items.Where(x => x.Name.ToLower().StartsWith(query.ToLower())));
        }

        private void Clear()
        {
            SelectedCompany = null;
            Name = "";
            Address = "";
        }
        private void Update()
        {
            if (SelectedCompany !=null)
            {
                SelectedCompany.Name = Name;
                SelectedCompany.Address = Address;
                repo.UpdateCompany(SelectedCompany);
            }

        }
        
        private void CreateCompany()
        {
          repo.CreateCompany(Name, Address);
        }
        internal void CreateCompany(Company newCompany)
        {
            throw new NotImplementedException();
        }
        //public void UpdateCompany()
        //{
        //}
        public List<Company> GetAllCompanies() 
        {
            repo.SearchCompany(string.Empty);
                var companies = new List<Company>();
        foreach (var company in repo)
                companies.Add(company);
        return companies;
        }
        internal void UpdateCompany (Company companytoUpdate)
        {
            throw new NotImplementedException();
        }
        

    }
}