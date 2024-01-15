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
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private byte[]? imageData;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private string name = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private string type = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private Product? selectedProduct;

        partial void OnSelectedProductChanged(Product? value)
        {
            Name = value?.Name ?? "";
            Type = value?.Type ?? "";
            ImageData = value?.ImageData;
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

        private ProductRepository productRepository = new();
        public ProductsPageViewModel()
        {
            Items = productRepository.Items;
            productRepository.RepositoryChanged += (_, list) =>
            {
                Items = new(list);
            };

            SearchCommand = new RelayCommand<string>(Search);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            CreateCommand = new RelayCommand(CreateProduct, IsDataValid);
            ResetCommand = new RelayCommand(Clear);
            ChooseFileCommand = new AsyncRelayCommand(ChooseFile);
        }

        private void Clear()
        {
            SelectedProduct = null;
        }

        private void Update()
        {
            SelectedProduct.Name = Name;
            SelectedProduct.Type = Type;
            SelectedProduct.ImageData = ImageData;
            productRepository.Update(SelectedProduct);
        }

        private void Search(string query)
        {
            SearchItems = new(Items.Where(x => x.Name.ToLower().StartsWith(query.ToLower())));
        }


        private void CreateProduct()
        {
            SelectedProduct = productRepository.Create(Type, Name, ImageData!);
        }

        private async Task ChooseFile()
        {
            PickOptions options = new PickOptions();
            options.FileTypes = FilePickerFileType.Images;
            FileResult? result = await FilePicker.Default.PickAsync(options);
            ImageData = result is null ? null : Product.GetImageData(result.FullPath);
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
