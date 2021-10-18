using FleetManager.WebAPI.Data;
using FleetManager.WebAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace FleetManager.WebAPI
{
    public class MemoryDataContext : ITypedDataContext
    {
        #region Singleton

        private static MemoryDataContext _instance;

        public static MemoryDataContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MemoryDataContext();
                }
                return _instance;
            }
        }
        #endregion

        private int _currentCarId = 16;
        private int _currentLocationId = 2;

        private readonly IList<Car> _cars;
        private readonly IList<Location> _locations;

        private MemoryDataContext()
            //Data der kun anvendes under test. Det hele skal knyttes til en database senere (SQL)
        {
            _cars = new List<Car>()
            {
                new Car() { Id = 1, Brand = "Ford", Mileage = 12398, Location = new Location(){ Id = 1, Name = "Aalborg" } },
                new Car() { Id = 2, Brand = "Skoda", Mileage = 5466, Location = new Location(){ Id = 2, Name = "Randers" } },
                new Car() { Id = 5, Brand = "Renault", Mileage = 50, Location = new Location(){ Id = 1, Name = "Aalborg" } },
                new Car() { Id = 8, Brand = "Mazda", Mileage = 120506, Location = new Location(){ Id = 2, Name = "Randers" } },
                new Car() { Id = 10, Brand = "Toyota", Mileage = 9887, Location = new Location(){ Id = 2, Name = "Randers" } },
                new Car() { Id = 16, Brand = "KIA", Mileage = 345, Location = new Location(){ Id = 1, Name = "Aalborg" } },
            };
            _locations = new List<Location>()
            {
                new Location(){ Id = 1, Name = "Aalborg" },
                new Location(){ Id = 2, Name = "Randers" },
            };
        }

        public IEnumerable<Car> Cars => _cars;

        public IEnumerable<Location> Locations => _locations;

        public Car Add(Car car)
        {
            if (!car.Id.HasValue || (car.Id.HasValue && !_cars.Any(c => c.Id == car.Id)))
            {
                car.Id = GenerateId<Car>();
                _cars.Add(car);
                return car;
            }
            throw new DaoException("An error ocurred while adding car");
        }

        public Location Add(Location location)
        {
            if (!location.Id.HasValue || (location.Id.HasValue && !_locations.Any(l => l.Id == location.Id)))
            {
                location.Id = GenerateId<Location>();
                _locations.Add(location);
                return location;
            }
            throw new DaoException("An error ocurred while adding location");
        }

        public bool Remove(Car car)
        {
            if (car == null || !car.Id.HasValue)
                return false;
            return _cars.Remove(_cars.SingleOrDefault(c => c.Id == car.Id));
        }

        public bool Remove(Location location)
        {
            if (location == null || !location.Id.HasValue)
                return false;
            return _locations.Remove(_locations.SingleOrDefault(c => c.Id == location.Id));
        }

        public int GenerateId<TModel>()
        {
            return typeof(TModel) switch
            {
                var model when model == typeof(Car) => ++_currentCarId,
                var model when model == typeof(Location) => ++_currentLocationId,
                _ => 0
            };
        }
    }
}
