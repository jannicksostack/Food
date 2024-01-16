using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
        private int activeProductId;

        [ObservableProperty]
        private int quantity;

        [ObservableProperty]
        private decimal total;

        public OrderDetails(SqlDataReader reader)
        {
            OrderId = (int) reader["OrdreID"];
            ActiveProductId = (int) reader["AktiveProduktID"];
            Quantity = (int) reader["Mængde"];
            Total = (decimal) reader["TotalBeløb"];
        }
    }
}
