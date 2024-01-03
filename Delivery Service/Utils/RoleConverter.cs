using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Utils {
    public static class RoleConverter {
        public static string ConvertRole(string role) {
            if (role == "Admin") return "Администратор";
            if (role == "Courier") return "Курьер";
            else return role;
        }
    }
}
