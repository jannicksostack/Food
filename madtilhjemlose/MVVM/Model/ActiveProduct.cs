using CommunityToolkit.Mvvm.ComponentModel;
using madtilhjemlose.MVVM.Model.User;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private DateTime date;
        [ObservableProperty]
        private int quantity;
        [ObservableProperty]
        private decimal price;

        public ActiveProduct(SqlDataReader reader, ObservableCollection<Product> products)
        {
            Id = (int) reader["AktiveProduktID"];
            Date = (DateTime) reader["AktiveProduktUdløbsdato"];
            Quantity = (int) reader["AktiveProduktMaxMængde"];
            Price = (decimal) reader["AktiveProduktPris"];

            int productId = (int) reader["ProduktID"];
            Product = products.First(x => x.Id == productId);
        }
    }
}
