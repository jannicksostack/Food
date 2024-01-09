using madtilhjemlose.MVVM.DataAccess;
using Microsoft.Data.SqlClient;
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
        public override UserType UserType => UserType.Admin;
        private UserRepository userRepo = new();
        private ContractRepository contractRepo = new();
        private ProductRepository projectRepo = new();

        public AdminUser(int id, int companyId) : base(id, companyId) { }
        public static AdminUser FromReader(SqlDataReader reader) {
            int id = (int) reader["BrugerID"];
            int companyId = (int) reader["FirmaID"];
            return new AdminUser(id, companyId);
        }
    }
}
