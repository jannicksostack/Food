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

        public AdminUser(int id, int companyId) : base(id, companyId) { }
        public static AdminUser FromReader(SqlDataReader reader) {
            int id = (int) reader["BrugerID"];
            int companyId = (int) reader["FirmaID"];
            return new AdminUser(id, companyId);
        }

        public bool CreateProduct(string type, string name, string imagePath)
        {
            return productRepo.CreateProduct(type, name, imagePath);
        }

        public List<Product> GetProducts()
        {
            return productRepo.GetProducts();
        }
    }
}
