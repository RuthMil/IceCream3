using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IceCream3.Models;

namespace IceCream3.Data
{
    public class IceCream3Context : DbContext
    {
        public IceCream3Context (DbContextOptions<IceCream3Context> options)
            : base(options)
        {
        }

        public DbSet<IceCream3.Models.Menu> Menu { get; set; }

        public DbSet<IceCream3.Models.Order> Order { get; set; }
    }
}
