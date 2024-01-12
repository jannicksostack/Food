using madtilhjemlose.MVVM.DataAccess;
using Microsoft.Data.SqlClient;
using madtilhjemlose.MVVM.ViewModel;
using madtilhjemlose.MVVM.View;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace madtilhjemlose.MVVM.Model.User
{
    public class AdminUser : BaseUser
    {
        public static AdminUser CurrentUser => App.Current!.MainPage!.Navigation.NavigationStack
            .Where(x => x is View.User.Admin.MainPage)
            .Select(x => x as View.User.Admin.MainPage)
            .Select(x => x!.BindingContext as ViewModel.User.Admin.MainPageViewModel)
            .First()!
            .User;
            
        public override UserType UserType => UserType.Admin;
        private UserRepository userRepo = new();
        private ContractRepository contractRepo = new();
        private ProductRepository productRepo = new();
        private ActiveProductRepository activeProductRepo;

        public event EventHandler<ObservableCollection<Product>> ProductsChanged;
        public ObservableCollection<Product> Products;

        public event EventHandler<ObservableCollection<ActiveProduct>> ActiveProductsChanged;
        public ObservableCollection<ActiveProduct> ActiveProducts;

        public AdminUser(int id, int companyId) : base(id, companyId) {
            activeProductRepo = new(productRepo);

            Products = GetProducts();
            ActiveProducts = GetActiveProducts();
            productRepo.RepositoryChanged += (_, list) =>
            {
                Products = new(list);
                ProductsChanged?.Invoke(this, list);
            };
            activeProductRepo.RepositoryChanged += (_, list) =>
            {
                ActiveProducts = new(list);
                ActiveProductsChanged?.Invoke(this, list);
            };
        }
        public static AdminUser FromReader(SqlDataReader reader) {
            int id = (int) reader["BrugerID"];
            int companyId = (int) reader["FirmaID"];
            return new AdminUser(id, companyId);
        }

        public Product CreateProduct(string type, string name, byte[]? imageData)
        {
            return productRepo.CreateProduct(type, name, imageData);
        }

        public ActiveProduct? CreateActiveProduct(Product product, DateTime date, int quantity, decimal price)
        {
            return activeProductRepo.Create(product, date, quantity, price);
        }

        public void UpdateProduct(Product product)
        {
            productRepo.UpdateProduct(product);
        }

        public ObservableCollection<Product> GetProducts()
        {
            return productRepo.GetProducts();
        }
        public ObservableCollection<ActiveProduct> GetActiveProducts()
        {
            return activeProductRepo.GetAll();
        }

        public void UpdateActiveProduct(ActiveProduct activeProduct)
        {
            activeProductRepo.Update(activeProduct);
        }
    }
}
