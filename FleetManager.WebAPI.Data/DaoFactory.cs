using FleetManager.WebAPI.Model;
using System;
using System.Data;

namespace FleetManager.WebAPI.Data
{
    public static class DaoFactory
    {
        public static IDao<TModel> Create<TModel>(IDataContext dataContext) //IDataContext er polymorfi her
        {
            Type dataContextType = dataContext.GetType(); //Typen trækkes ud med GetType()

            // TODO: (Step 3) add check for the sql server datacontext interface type and return the correct dao

            if (typeof(ITypedDataContext).IsAssignableFrom(dataContextType)) //Er dette sandt, så køres blokken af koden under
            {
                return typeof(TModel) switch
                {
                    var dao when dao == typeof(Car) => new Daos.Memory.CarDao(dataContext as ITypedDataContext) as IDao<TModel>,
                    var dao when dao == typeof(Location) => new Daos.Memory.LocationDao(dataContext as ITypedDataContext) as IDao<TModel>,
                    _ => throw new DaoFactoryException($"Model [{typeof(TModel).Name}] not supported"),
                };
            }
            else if (typeof(IDataContext<IDbConnection>).IsAssignableFrom(dataContextType))
            {
                return typeof(TModel) switch
                {
                    var dao when dao == typeof(Car) => new Daos.SQL.CarDao(dataContext as IDataContext<IDbConnection>) as IDao<TModel>,
                    var dao when dao == typeof(Location) => new Daos.SQL.LocationDao(dataContext as IDataContext<IDbConnection>) as IDao<TModel>,
                    _ => throw new DaoFactoryException($"Model [{typeof(TModel).Name}] not supported"),
                };
            }
            throw new DaoFactoryException($"DataContext [{dataContext}] not supported");
        }
    }
}
