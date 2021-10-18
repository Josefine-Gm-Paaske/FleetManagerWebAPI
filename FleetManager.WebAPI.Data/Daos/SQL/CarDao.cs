﻿using FleetManager.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FleetManager.WebAPI.Data.Daos.SQL
{
    // TODO: (Step 4) implement data access object for the Cars table in the SQL Server database

    // 1. make the class inherit from the BaseDao class and use the relevant data context interface as type parameter
    // 2. implement the IDao interface in the class with the Car model class as type parameter

    class CarDao : BaseDao<IDataContext<IDbConnection>>, IDao<Car>
    {
        public CarDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public Car Create(Car model)
        {
            string query = "INSERT INTO[Cars] (brand, mileage) VALUES(@brand, @mileage)";
            using IDbConnection connection = DataContext.Open();
            //Ville være bedre med model i stedet for new anonym type, da der ikke er et id med denne løsning
            connection.Query<Car>(query, new
            {
                brand = model.Brand,
                mileage = model.Mileage,
            });
            return model;

            //using IDbConnection connection = DataContext.Open();
            //Car temp = model;
            //temp.Id = Int32.Parse(connection.ExecuteScalar("INSERT INTO [Cars] ([Brand],[Milage],[LocationId]) VALUES (@Brand, @Milage @LocationId); SELECT SCOPE_IDENTITY();", model).ToString());
            //return model;
        }

        public bool Delete(Car model)
        {
            string query = "DELETE Cars WHERE id = @id";
            //"Using" her disposes, når vi kommer ud af scope
            //IDbConnection arver fra IDisposable
            using IDbConnection connection = DataContext.Open();
            return !(connection.Query<Car>(query, new
            {
                id = model.Id
            }).Any());
        }

        public IEnumerable<Car> Read()
        {
            string query = "SELECT * FROM Cars";
            //"Using" her disposes, når vi kommer ud af scope
            //IDbConnection arver fra IDisposable
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Car>(query);
        }
        //Objekt, der kommer ud af scope, ender i Garbagecollectoren

        public IEnumerable<Car> Read(Func<Car, bool> predicate)
        {
            string query = "SELECT * FROM Cars";
            //"Using" her disposes, når vi kommer ud af scope
            //IDbConnection arver fra IDisposable
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Car>(query).Where(predicate);
        }

        public bool Update(Car model)
        {
            string query = "UPDATE Cars SET brand = @brand WHERE id = @id";
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Car>(query, new { brand = model.Brand, id = model.Id }).Any();
        }
    }
}
