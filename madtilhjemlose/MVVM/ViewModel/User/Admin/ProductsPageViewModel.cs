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
        }

        partial void OnImageDataChanged(byte[]? value)
        {
            CreateCommand.NotifyCanExecuteChanged();
        }

        partial void OnTypeChanged(string value)
        {
            CreateCommand.NotifyCanExecuteChanged();
        }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public AsyncRelayCommand ChooseFileCommand { get; set; }


        [ObservableProperty]
        private ObservableCollection<Product> items;

        public ProductsPageViewModel()
        {
            SearchCommand = new RelayCommand(Search);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            CreateCommand = new RelayCommand(CreateProduct, IsDataValid);
            ChooseFileCommand = new AsyncRelayCommand(ChooseFile);

            Items = AdminUser.CurrentUser.GetProducts();
        }

        private void Update()
        {
            SelectedProduct.Name = Name;
            SelectedProduct.Type = Type;
            SelectedProduct.ImageData = ImageData;
            AdminUser.CurrentUser.UpdateProduct(SelectedProduct!);
        }

        private void Search()
        {

        }


        private void CreateProduct()
        {
           AdminUser.CurrentUser.CreateProduct(Type, Name, ImageData!);
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
