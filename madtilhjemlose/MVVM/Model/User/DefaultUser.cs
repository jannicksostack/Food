using madtilhjemlose.MVVM.DataAccess;
using Microsoft.Data.SqlClient;

namespace madtilhjemlose.MVVM.Model.User
{
    public class DefaultUser : RestrictedUser
    {
        private ContractRepository contractRepo = new();
        private OrderRepository orderRepository = new();
        public override UserType UserType => UserType.Default;
        public DefaultUser(int id, int companyId, string userName, string? email, string? phoneNumber, string? address) : base(id, companyId, userName, email, phoneNumber, address) { }
        public static DefaultUser FromReader(SqlDataReader reader)
        {
            int id = (int) reader["BrugerID"];
            int companyId = (int) reader["FirmaID"];
            string userName = (string) reader["BrugerNavn"];
            string? email = reader["BrugerEmail"]?.ToString();
            string? phoneNumber = "222"; // (string?) reader["UserPhone"];
            string? address = "Home";// (string?) reader["UserAddress"];

            return new DefaultUser(id, companyId, userName, email, phoneNumber, address);
        }
    }
}
