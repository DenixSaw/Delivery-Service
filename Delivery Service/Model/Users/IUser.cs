using System;

namespace Delivery_Service.Model.Users;
public interface IUser
{
    Guid Id { get; }
    string Name { get; set; }
    string Phone { get; set; }
    string Password { get; }
    string Role { get; }

}
