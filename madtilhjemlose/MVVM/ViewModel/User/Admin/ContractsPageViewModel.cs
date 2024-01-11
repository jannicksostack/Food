using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.View.User.Admin;
using System.ComponentModel;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class ContractsPageViewModel : ObservableValidator, INotifyPropertyChanged
    {
        private INavigation navigation;
        protected static ContractRepository repository = [];
        private Contract _contract;

        public ICommand SearchCommand { get; set; }
        public ICommand CreateContractCommand { get; set; }

        public ContractsPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            SearchCommand = new RelayCommand(Search);
            CreateContractCommand = new RelayCommand(CreateContract);
            ContractList = GetAllContracts();
            _contract = ContractList[0];
        }

        private void CreateContract()
        {
            navigation.PushAsync(new CreateContractPage());
        }

        private void Search()
        {

        }

        public List<Contract> ContractList { get; set; }
        private List<Contract> GetAllContracts()
        {
            repository.Search(string.Empty);
            var contracts = new List<Contract>();

            foreach (var contract in repository)
            {
                contracts.Add(contract);
            }

            // get all contract names from db and fill it in this.contracts 
            // Order by asending 

            return contracts;
        }
        
        public string SelectedContract
        {
            get { return _contract.CompanyName; }
            set 
            {
                if(SelectedContract != value)
                {
                    _contract.CompanyName = value;
                    // finds what contract holds the info selected from picker
                    for (int i = 0; i < ContractList.Count; i++)
                    {
                        if (ContractList[i].CompanyName == SelectedContract)
                        {
                            _contract.CompanyName = ContractList[i].CompanyName;
                            _contract.ContractID = ContractList[i].ContractID;
                            _contract.ContractStart = ContractList[i].ContractStart;
                            _contract.ContractEnd = ContractList[i].ContractEnd;
                            _contract.CompanyAddress = ContractList[i].CompanyAddress;
                            OnPropertyChanged(nameof(SelectedContract));
                            OnPropertyChanged(nameof(SelectedCompanyName));
                            break;
                        }
                    }
                }
            }
        }
        
        public string SelectedCompanyName
        {
            get { return _contract.CompanyName; }
            set
            {
                if( _contract.CompanyName != value)
                {
                    _contract.CompanyName = value;
                    OnPropertyChanged(nameof(SelectedCompanyName));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
