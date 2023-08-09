using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataBaseConection
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base(ConfigurationManager.ConnectionStrings["databaseContext"].ConnectionString) { }
    }
}
