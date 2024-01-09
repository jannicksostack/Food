using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class ProductsPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand CreateCommand { get; set; }

        public ProductsPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            SearchCommand = new RelayCommand(Search);
            DeleteCommand = new RelayCommand(Delete);
            UpdateCommand = new RelayCommand(Update);
            CreateCommand = new RelayCommand(Create);
        }

        private void Create()
        {
            navigation.PushAsync(new CreateProductPage());
        }

        private void Update()
        {
            navigation.PushAsync(new UpdateProductPage());
        }

        private void Delete()
        {

        }

        private void Search()
        {

        }
    }
}
