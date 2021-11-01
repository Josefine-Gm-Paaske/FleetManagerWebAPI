using FleetManager.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.WebAPI.Data.Daos.Memory
{
    /// <summary>
    /// In memory data access object for system test of the application
    /// </summary>
    class CarDao : BaseDao<ITypedDataContext>, IDao<Car>
    {
        public CarDao(ITypedDataContext dataContext) : base(dataContext)
        {
        }

        public Car Create(Car model)
        {
            DataContext.Add(model);
            return model;
        }

        public bool Delete(Car model)
        {
            Car deleted = DataContext.Cars.SingleOrDefault(c => c.Id == model.Id);
            return DataContext.Remove(deleted);
        }

        public IEnumerable<Car> Read()
        {
            return DataContext.Cars;
        }

        public IEnumerable<Car> Read(Func<Car, bool> predicate)
        {
            return DataContext.Cars.Where(predicate);
        }

        public bool Update1(Car model)
        {
            Car oldCar = DataContext.Cars.SingleOrDefault(c => c.Id == model.Id);
            if (DataContext.Remove(oldCar))
            {
                DataContext.Add(model);
                return true;
            }
            return false;
        }
    }
}
