using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fitbod.Models;

namespace Fitbod.Data
{
    public class FitbodContext : DbContext
    {
        public FitbodContext (DbContextOptions<FitbodContext> options)
            : base(options)
        {
        }

    }
}
