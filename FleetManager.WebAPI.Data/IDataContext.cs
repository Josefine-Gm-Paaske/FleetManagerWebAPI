using FleetManager.WebAPI.Model;
using System.Collections.Generic;

namespace FleetManager.WebAPI.Data
{
    public interface IDataContext
    {
        //Skal stå tomt. Det er en type, der snakker med data context 

    }
 
    /// <summary>
    /// Interface for generic connections to a datasource
    /// </summary>
    /// <typeparam name="TConnection">Type of connection (e.g., IDbConnection for a database connection)</typeparam>
    public interface IDataContext<TConnection> : IDataContext //Generisk IDataContext
    {
        TConnection Open();
    }

    public interface ITypedDataContext : IDataContext
    {
        /**
         * ITypedData er en collection. Her er en Cars Collection og en Location Collection
         * Begge interfaces arver fra IDataContext, så man kan lave instancer af begge på en gang
         */
        IEnumerable<Car> Cars { get; }
        IEnumerable<Location> Locations { get; }

        Car Add(Car car);
        Location Add(Location location);

        bool Remove(Car car);
        bool Remove(Location location);
    }
}
