using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.View.User.Admin;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal partial class ActiveProductsPageViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private string quantity = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private string price = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private Product? selectedProduct;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private ActiveProduct? selectedActiveProduct;

        [ObservableProperty]
        private ObservableCollection<Product> products;


        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public AsyncRelayCommand ChooseFileCommand { get; set; }


        [ObservableProperty]
        private ObservableCollection<ActiveProduct> items;

        partial void OnItemsChanged(ObservableCollection<ActiveProduct>? oldValue, ObservableCollection<ActiveProduct> newValue)
        {
            SearchItems = new(newValue);
        }

        partial void OnSelectedActiveProductChanged(ActiveProduct? value)
        {
            if (value is null)
            {
                return;
            }

            Quantity = value.Quantity.ToString();
            Price = value.Price.ToString();
            Date = value.Date;
            SelectedProduct = Products.First(x => x.Id == value.Product.Id);
        }

        [ObservableProperty]
        private ObservableCollection<ActiveProduct> searchItems;

        public ActiveProductsPageViewModel()
        {
            Products = new(AdminUser.CurrentUser.GetProducts().OrderBy(x => x.Name));
            Items = new(AdminUser.CurrentUser.ActiveProducts);
            AdminUser.CurrentUser.ActiveProductsChanged += (_, list) =>
            {
                Items = new(list);
            };

            SearchCommand = new RelayCommand<string>(Search);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            CreateCommand = new RelayCommand(Create, IsDataValid);
            ResetCommand = new RelayCommand(Clear);

        }

        private void Clear()
        {
            SelectedProduct = null;
            Quantity = "";
            Price = "";
            Date = DateTime.Now;
            SelectedActiveProduct = null;
        }

        private void Update()
        {
            SelectedActiveProduct.Product = SelectedProduct;
            SelectedActiveProduct.Date = Date;
            SelectedActiveProduct.Quantity = Convert.ToInt32(Quantity);
            SelectedActiveProduct.Price = Convert.ToDecimal(Price.Replace('.', ','));
            AdminUser.CurrentUser.UpdateActiveProduct(SelectedActiveProduct);
        }

        private void Search(string? query)
        {
            SearchItems = new(Items.Where(x => x.Product.Name.ToLower().StartsWith(query.ToLower())));
        }


        private void Create()
        {
            SelectedActiveProduct = AdminUser.CurrentUser.CreateActiveProduct(SelectedProduct, Date, Convert.ToInt32(Quantity), Convert.ToDecimal(Price.Replace('.',',')));
        }

        private bool IsDataValid()
        {
            bool isInt = Int32.TryParse(Quantity, out int _);
            bool isDecimal = Decimal.TryParse(Price, out decimal _);
            return SelectedProduct is not null && isInt && isDecimal && Date.Date >= DateTime.Now.Date;
        }

        private bool CanUpdate()
        {
            return IsDataValid() && SelectedActiveProduct is not null;
        }
    }
}
