using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2
{
    public class HashContext : DbContext
    {
        public DbSet<HashModel> HashTable { get; set; }
    }
}
