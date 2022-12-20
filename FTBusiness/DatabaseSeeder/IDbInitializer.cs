using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTBusiness.DatabaseSeeder
{
    public interface IDbInitializer
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        Task Initialize();
        /// <summary>
        /// Initial seeding methods for super admin 
        /// </summary>
        /// <returns></returns>
        Task SeedData();

    }
}
