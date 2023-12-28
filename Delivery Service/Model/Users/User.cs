using System;
using Delivery_Service.Model.Users;

namespace Delivery_Service.Model
{
    public class User : IUser
    {

        public Guid Id { get; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; }
        public string Role { get; }

        public User(Guid Id, string Phone, string Name, string Password, string Role)
        {
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;
            this.Password = Password;
            this.Role = Role;
        }

    }
}
