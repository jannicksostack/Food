using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.Model.User;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal partial class CreateProductPageViewModel : ObservableValidator
    {
        [ObservableProperty]
        private string? imagePath;
        [ObservableProperty]
        private string name = "";
        [ObservableProperty]
        private string type = "";

        public ICommand ChooseFileCommand { get; set; }
        public ICommand CreateCommand { get; set; }

        public CreateProductPageViewModel()
        {
            ChooseFileCommand = new AsyncRelayCommand(ChooseFile);
            CreateCommand = new RelayCommand(CreateProduct);
        }

        private void CreateProduct()
        {
            bool created = AdminUser.CurrentUser.CreateProduct(Name, Type, ImagePath!);
            if (created)
            {
                App.Navigation.PopAsync();
            }
        }

        private async Task ChooseFile()
        {
            PickOptions options = new PickOptions();
            options.FileTypes = FilePickerFileType.Images;
            FileResult? result = await FilePicker.Default.PickAsync(options);
            ImagePath = result?.FullPath;
        }
    }
}
