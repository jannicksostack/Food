using CommunityToolkit.Mvvm.ComponentModel;
using madtilhjemlose.MVVM.Model.User;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.Model
{
    public partial class ActiveProduct : ObservableObject
    {
        [ObservableProperty]
        private int id; 
        [ObservableProperty]
        private Product product;
        [ObservableProperty]
        private DateOnly date;
        [ObservableProperty]
        private int quantity;
        [ObservableProperty]
        private decimal price;

        public ActiveProduct(SqlDataReader reader)
        {
            Id = (int) reader["AktiveProduktID"];
            Date = (DateOnly) reader["AktiveProduktUdløbsdato"];
            Quantity = (int) reader["AktiveProduktMaxMængde"];
            Price = (decimal) reader["AktiveProduktPris"];

            int productId = (int) reader["ProductID"];
            Product = AdminUser.CurrentUser.Products.First(x => x.Id == productId);
        }
    }
}
