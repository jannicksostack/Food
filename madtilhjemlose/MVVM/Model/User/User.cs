using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.Model.User
{
    public class User
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? HomePhone { get; set; }
        public string? WorkPhone { get; set; }
        public string? Address { get; set; }

        public UserType UserType { get; set; }

        public static User Current { get; set; }

        public User(SqlDataReader reader)
        {
            Id = (int) reader["BrugerID"];
            CompanyId = (int) reader["FirmaID"];
            UserName = (string) reader["BrugerNavn"];
            Email = reader.IsDBNull("BrugerEmail") ? null : (string) reader["BrugerEmail"];
            HomePhone = reader.IsDBNull("BrugerHjemmetelefon") ? null : (string) reader["BrugerHjemmetelefon"];
            WorkPhone = reader.IsDBNull("BrugerArbejdstelefon") ? null : (string) reader["BrugerArbejdstelefon"];
            Address = reader.IsDBNull("BrugerAdresse") ? null : (string) reader["BrugerAdresse"];
            UserType = ((string)reader["BrugerType"]).ToUserType();
        }
    }
}
