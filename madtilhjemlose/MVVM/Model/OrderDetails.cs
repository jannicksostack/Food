using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.Model
{
    public partial class OrderDetails : ObservableObject
    {
        [ObservableProperty]
        private int orderId;

        [ObservableProperty]
        private ActiveProduct activeProduct;

        [ObservableProperty]
        private int quantity;

        [ObservableProperty]
        private decimal total;

        public OrderDetails(SqlDataReader reader, ObservableCollection<ActiveProduct> activeProducts)
        {
            OrderId = (int) reader["OrdreID"];
            int activeProductId = (int) reader["AktiveProduktID"];
            ActiveProduct = activeProducts.Single(x => x.Id == activeProductId);
            Quantity = (int) reader["Mængde"];
            Total = (decimal) reader["TotalBeløb"];
        }
    }
}
