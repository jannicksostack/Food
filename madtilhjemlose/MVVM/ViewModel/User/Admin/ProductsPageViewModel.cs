using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.Model.User;
using madtilhjemlose.MVVM.View.User.Admin;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal partial class ProductsPageViewModel : ObservableValidator
    {
        [ObservableProperty]
        private byte[]? imageData;
        [ObservableProperty]
        private string name = "";
        [ObservableProperty]
        private string type = "";

        [ObservableProperty]
        private Product? selectedProduct;

        partial void OnSelectedProductChanged(Product? value)
        {
            if (value is not null)
            {
                Name = value.Name;
                Type = value.Type;
                ImageData = value.ImageData;
            }
            UpdateCommand.NotifyCanExecuteChanged();
        }

        partial void OnNameChanged(string value)
        {
            CreateCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }

        partial void OnImageDataChanged(byte[]? value)
        {
            CreateCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }

        partial void OnTypeChanged(string value)
        {
            CreateCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }

        partial void OnItemsChanged(ObservableCollection<Product> value)
        {
            SearchItems = value;
        }

        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public AsyncRelayCommand ChooseFileCommand { get; set; }


        [ObservableProperty]
        private ObservableCollection<Product> items;

        [ObservableProperty]
        private ObservableCollection<Product> searchItems;

        public ProductsPageViewModel()
        {
            AdminUser.CurrentUser.ProductsChanged += OnProductsChanged;
            AdminUser.CurrentUser.GetProducts();

            SearchCommand = new RelayCommand<string>(Search);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            CreateCommand = new RelayCommand(CreateProduct, IsDataValid);
            ResetCommand = new RelayCommand(Clear);
            ChooseFileCommand = new AsyncRelayCommand(ChooseFile);

        }

        private void OnProductsChanged(object? sender, ObservableCollection<Product> list)
        {
            Items = list;
        }

        private void Clear()
        {
            SelectedProduct = null;
            Name = "";
            Type = "";
            ImageData = null;
        }

        private void Update()
        {
            SelectedProduct.Name = Name;
            SelectedProduct.Type = Type;
            SelectedProduct.ImageData = ImageData;
            AdminUser.CurrentUser.UpdateProduct(SelectedProduct!);
        }

        private void Search(string query)
        {
            SearchItems = new(Items.Where(x => x.Name.ToLower().StartsWith(query.ToLower())));
        }


        private void CreateProduct()
        {
            SelectedProduct = AdminUser.CurrentUser.CreateProduct(Type, Name, ImageData!);
        }

        private async Task ChooseFile()
        {
            PickOptions options = new PickOptions();
            options.FileTypes = FilePickerFileType.Images;
            FileResult? result = await FilePicker.Default.PickAsync(options);
            ImageData = Product.GetImageData(result?.FullPath);
        }

        private bool IsDataValid()
        {
            return ImageData is not null && !Name.IsNullOrEmpty() && !Type.IsNullOrEmpty();
        }

        private bool CanUpdate()
        {
            return IsDataValid() && SelectedProduct is not null;
        }
    }
}
