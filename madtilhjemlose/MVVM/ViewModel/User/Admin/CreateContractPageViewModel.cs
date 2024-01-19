using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Drawing;
using System.Drawing.Printing;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Maui.Controls.Shapes;
using System.Drawing.Text;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using CommunityToolkit.Mvvm.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal partial class CreateContractPageViewModel : ObservableValidator, INotifyPropertyChanged
    {
        private INavigation navigation;
        private Contract _contract;
        protected static ContractRepository repository = [];

        string companyName;
        string companyAddress;
        string contractBeginDate;
        string contractEndDate;
        string userName;
        string password;

        public ICommand CreateContractCommand {  get; set; }

        public CreateContractPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            CreateContractCommand = new RelayCommand(CreateContractWithUser);
            
        }

        private void CreateContractWithUser()
        {
            if (companyName != null && companyAddress != null && contractBeginDate != null && contractEndDate != null
                && userName != null && password != null)
            {
                repository.CreateContract(companyName, companyAddress, contractBeginDate, contractEndDate);
                // after Contract and Company is made, make a user that can by items. 
                UserRepository user = new UserRepository();
                // brugerTypeID = 2 is the standard buyer for a company
                // each company contract need 1 member with brugerTypeID = 2
                user.CreateUser(companyName, 2, userName, password);
            }
            
        }

        public string CompanyName
        {
            get => companyName;
            set
            {
                companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }
        public string CompanyAddress
        {
            get => companyAddress;
            set
            {
                companyAddress = value;
                OnPropertyChanged(nameof(CompanyAddress));
            }
        }
        public string ContractBeginDate
        {
            get => contractBeginDate;
            set
            {
                contractBeginDate = value;
                OnPropertyChanged(nameof(ContractBeginDate));
            }
        }
        public string ContractEndDate
        {
            get => contractEndDate;
            set
            {
                contractEndDate = value;
                OnPropertyChanged(nameof(ContractEndDate));
            }
        }
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
