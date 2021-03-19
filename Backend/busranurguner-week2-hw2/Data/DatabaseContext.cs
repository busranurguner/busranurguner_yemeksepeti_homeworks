using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_center.Data.Context
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

         public DbSet<Patient> Patients { get; set; }


    }
}
