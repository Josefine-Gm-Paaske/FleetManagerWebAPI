using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.WebAPI.Data
{
    public interface IDao<TModel>
    {
        /// <summary>
        /// Creates a new resource based on the values specified
        /// </summary>
        /// <returns>The instance of the created resource with the new id</returns>
        TModel Create(TModel model);

        /// <summary>
        /// Reads the resources
        /// </summary>
        /// <returns>An IEnumerable object with all the available resources</returns>
        IEnumerable<TModel> Read();

        /// <summary>
        /// Reads the resources
        /// </summary>
        /// <param name="predicate">A function that specifies wether a condition for a resource is true</param>
        /// <returns>An IEnumerable object with all the resources that satisfies the predicate</returns>
        IEnumerable<TModel> Read(Func<TModel, bool> predicate);

        /// <summary>
        /// Updates the specified resource
        /// </summary>
        /// <returns>A value indicating if the operation succeeded</returns>
        bool Update1(TModel model);

        /// <summary>
        /// Deletes the specified resource
        /// </summary>
        /// <returns>A value indicating if the operation succeeded</returns>
        bool Delete(TModel model);
    }
}
