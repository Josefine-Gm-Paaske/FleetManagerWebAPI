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
    class LocationDao : BaseDao<ITypedDataContext>, IDao<Location>
    {       
        public LocationDao(ITypedDataContext dataContext) : base(dataContext)
        {
        }

        public Location Create(Location model)
        {
            DataContext.Add(model);
            return model;
        }

        public bool Delete(Location model)
        {
            Location deletedLocation = DataContext.Locations.SingleOrDefault(l => l.Id == model.Id);
            return DataContext.Remove(deletedLocation);
        }

        public IEnumerable<Location> Read()
        {
            return DataContext.Locations;
        }

        public IEnumerable<Location> Read(Func<Location, bool> predicate)
        {
            return DataContext.Locations.Where(predicate);
        }

        public bool Update1(Location model)
        {
            if (Delete(model))
            {
                DataContext.Add(model);
                return true;
            }
            return false;
        }
    }
}
