using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class ContractsPageViewModel : ObservableValidator
    {
        private INavigation navigation;
        protected static ContractRepository repository;

        public ICommand SearchCommand { get; set; }
        public ICommand CreateContractCommand { get; set; }

        public ContractsPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            SearchCommand = new RelayCommand(Search);
            CreateContractCommand = new RelayCommand(CreateContract);
            ContractList = GetAllContracts();
        }

        private void CreateContract()
        {
            navigation.PushAsync(new CreateContractPage());
        }

        private void Search()
        {

        }

        public List<string> ContractList { get; set; }
        private List<string> GetAllContracts()
        {
            // repository.Search(string.Empty);
            var contracts = new List<string>();

            // get all contract names from db and fill it in this.contracts 
            // Order by asending 

            return contracts;
        }


    }
}
