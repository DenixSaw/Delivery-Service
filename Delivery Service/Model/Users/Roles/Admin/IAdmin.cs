using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Model.Users.Roles.Admin {
    public interface IAdmin {
        Guid UserId { get; }
    }
}
