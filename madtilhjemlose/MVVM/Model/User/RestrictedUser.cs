
using madtilhjemlose.MVVM.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;

namespace madtilhjemlose.MVVM.Model.User
{
    internal class RestrictedUser : BaseUser
    {
        public ProductRepository productRepo = new();

        public override UserType UserType => UserType.Restricted;
        public RestrictedUser(int id, int companyId) : base(id, companyId) { }
        public static RestrictedUser FromReader(SqlDataReader reader) {
            int id = (int) reader["id"];
            int companyId = (int) reader["companyId"];
            return new RestrictedUser(id, companyId);
        }
    }
}
