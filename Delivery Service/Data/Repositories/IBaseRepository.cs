using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Data.Repositories {
    public interface IBaseRepository<T> {
        public bool Add(T entity);
        public T? GetById(Guid id);
        public IList<T>? GetAll();
        public bool Delete(T entity);
        public bool Update(T entity);
    }
}
