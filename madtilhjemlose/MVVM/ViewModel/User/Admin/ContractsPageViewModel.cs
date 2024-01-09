using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class ContractsPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public ICommand SearchCommand { get; set; }
        public ICommand CreateContractCommand { get; set; }

        public ContractsPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            SearchCommand = new RelayCommand(Search);
            CreateContractCommand = new RelayCommand(CreateContract);
        }

        private void CreateContract()
        {
            navigation.PushAsync(new CreateContractPage());
        }

        private void Search()
        {

        }
    }
}
