
using madtilhjemlose.MVVM.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;

namespace madtilhjemlose.MVVM.Model.User
{
    public class RestrictedUser : BaseUser
    {
        protected ProductRepository productRepo = new();

        public override UserType UserType => UserType.Restricted;
        protected RestrictedUser(int id, int companyId, string userName, string? email, string? phoneNumber, string? address) : base(id, companyId, userName, email, phoneNumber, address) { }
        public RestrictedUser(int id, int companyId) : base(id, companyId) { }
        public static RestrictedUser FromReader(SqlDataReader reader) {
            int id = (int) reader["BrugerID"];
            int companyId = (int) reader["FirmaID"];
            return new RestrictedUser(id, companyId);
        }
    }
}
