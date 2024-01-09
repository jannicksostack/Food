using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.Model.User
{
    public enum UserType
    {
        Restricted,
        Default,
        Admin
    }
    public static class UserTypeExt
    {
        public static UserType ToUserType(this string value)
        {
            return value switch
            {
                "Restricted" => UserType.Restricted,
                "Default" => UserType.Default,
                "Admin" => UserType.Admin,
                _ => throw new InvalidDataException(value + " is not a valid UserType")
            };
        }

        public static string ToString(this UserType value)
        {
            return value.ToString();
        }
    }

}
