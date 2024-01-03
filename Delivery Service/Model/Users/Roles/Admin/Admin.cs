using Delivery_Service.Entities;
using Delivery_Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Delivery_Service.Model.Users.Roles.Admin;
namespace Delivery_Service.Model.Users.Roles.Admin;

public class Admin : IAdmin {
    public Guid UserId { get; }

    public Admin(Guid Id) {
        if (Id == Guid.Empty) throw new ArgumentException("Идентификатор пользователя не может быть пустым");
        this.UserId = Id;
    }

}
