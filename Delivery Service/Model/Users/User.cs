using System;
using System.Text.RegularExpressions;
using Delivery_Service.Model.Users;

namespace Delivery_Service.Model
{
    public class User : IUser
    {
        private static Regex phoneRegex = new(@"^[0-9]{10}$");
        public Guid Id { get; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; }
        public string Role { get; }

        public User(Guid Id, string Phone, string Name, string Password, string Role)
        {
            if (Id == Guid.Empty) throw new ArgumentException("Идентификтатор пользователя не может быть пустым");
            if (Name == null || Name.Length == 0) throw new ArgumentException("Имя пользователя не может быть пустым");
            if (!phoneRegex.IsMatch(Phone)) throw new ArgumentException("Телефон пользователя не соотвествует регулярному выражению");
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;
            this.Password = Password;
            this.Role = Role;
        }

    }
}
