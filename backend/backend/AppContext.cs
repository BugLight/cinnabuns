using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
        }
    }
}
