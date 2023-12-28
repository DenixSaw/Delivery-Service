using Delivery_Service.Data;
using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Services {
    public class DishCUDInteractor : IBaseCUDInteractor<IProduct> {
        private readonly IDataManager _dataManager;

        public DishCUDInteractor(IDataManager dataManager) {
            _dataManager = dataManager;
        }

        public IProduct? TryAdd(IProduct newDish) {
            if (_dataManager.DishRepository.Add(newDish)) { return newDish; } return null;
        }

        public bool TryDelete(IProduct dish) {
            if (dish != null && _dataManager.DishRepository.GetById(dish.Id) != null) {
                if (_dataManager.DishRepository.Delete(dish)) {
                    return true;
                }
            } return false;
        }

        public bool TryUpdate(IProduct dish) {
            if (_dataManager.DishRepository.Update(dish)) { return true; } return false;
        }

    }
}
