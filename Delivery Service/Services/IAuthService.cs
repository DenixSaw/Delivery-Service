using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Services {
    public interface IAuthService {
        public bool TrySignUp(string name, string phone, string password, string repeatedPassword, string role);
        public bool TrySignIn(string phone, string password, string role);
    }
}
