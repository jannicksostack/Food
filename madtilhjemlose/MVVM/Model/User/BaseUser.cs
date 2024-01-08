using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.Model.User
{
    public abstract class BaseUser
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public abstract UserType UserType { get; }

        protected BaseUser(int id, int companyId)
        {
            Id = id;
            CompanyId = companyId;
        }
        protected BaseUser(int id, int companyId, string userName, string? email, string? phoneNumber, string? address)
        {
            Id = id;
            CompanyId = companyId;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
