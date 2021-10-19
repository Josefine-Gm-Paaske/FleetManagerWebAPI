using FleetManager.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FleetManager.WebAPI.Data.Daos.SQL
{
    // TODO: (Step 4) implement data access object for the Locations table in the SQL Server database

    // 1. make the class inherit from the BaseDao class and use the relevant data context interface as type parameter
    // 2. implement the IDao interface in the class with the Location model class as type parameter

    class LocationDao : BaseDao<IDataContext<IDbConnection>>, IDao<Location>
    {
        public LocationDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {

        }

        public Location Create(Location model)
        {
            string query = "INSERT INTO[Locations] (name)" + "VALUES(@name); SELECT SCOPE_IDENTITY; ";
            using IDbConnection connection = DataContext.Open();
            connection.Query<Location>(query, new
            {
                name = model.Name
            });
            return model;
            //using IDbConnection connection = DataContext.Open();
            //Location temp = model;
            //temp.Id = Int32.Parse(connection.ExecuteScalar("INSERT INTO [Locations] (Id, Name) VALUES (@Id, @Name); SELECT SCOPE_IDENTITY();", model).ToString());
            //return model;
        }

        public bool Delete(Location model)
        {
            string query = "DELETE Locations WHERE id = @id";
            //"Using" her disposes, når vi kommer ud af scope
            //IDbConnection arver fra IDisposable
            using IDbConnection connection = DataContext.Open();
            return !(connection.Query<Location>(query, new
            {
                id = model.Id
            }).Any());
        }

        public IEnumerable<Location> Read()
        {
            string query = "SELECT * FROM Locations";
            //"Using" her disposes, når vi kommer ud af scope
            //IDbConnection arver fra IDisposable
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Location>(query);
        }

        public IEnumerable<Location> Read(Func<Location, bool> predicate)
        {
            string query = "SELECT * FROM Locations";
            //"Using" her disposes, når vi kommer ud af scope
            //IDbConnection arver fra IDisposable
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Location>(query).Where(predicate);
        }

        public bool Update(Location model)
        {
            string query = "UPDATE Locations SET name = @name WHERE id=@id";
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Location>(query, new { name = model.Name, id = model.Id }).Any();
        }
    }
}
