using Delivery_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Services {
    public interface IBaseCUDInteractor<T> {
        public T? TryAdd(IProduct item);

        public bool TryUpdate(T item);
        public bool TryDelete(T item);
    }
}
