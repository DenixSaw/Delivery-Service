using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Utils {
    public interface ICalculator {
        public decimal Calculate(IList<ICartObject> cartObjects);
    }
}
