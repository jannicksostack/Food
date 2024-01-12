using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace madtilhjemlose.MVVM.Model
{
    public partial class Company : ObservableObject
    {

        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string address;

        private SqlDataReader reader;

        public Company(SqlDataReader reader)
        {

            Id = (int)reader ["FirmaId"];
            Name = (string)reader["FirmaNavn"];
            Address = (string)reader["FirmaAdresse"];

        }

        

        public Company(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

       
    }
}
