using FleetManager.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManager.WebAPI
{//Denne klasse svarer til DataContext klassen
    public class SQLServerDataContext : IDataContext<IDbConnection>
    {   /**
         * DataSource er en server streng, Initial Catalog er databasen, Integrated Sercurity anvender sikkerheden man er logget ind med
         */
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FleetManager;Integrated Security=True";
        public IDbConnection Open()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
