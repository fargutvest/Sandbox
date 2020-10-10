using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;

namespace Shop.DAL
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("DbConnection") { }
        
        public DbSet<Shop.Models.Product> Product { get; set; }
    }
}
