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
        public DbSet<UserModel> User { get; set; } = default!;

        public DbSet<RoleModel> Role { get; set; }

        public DbSet<Fitbod.Models.DishModel> DishModel { get; set; }

        public DbSet<Fitbod.Models.WeekDayModel> WeekDayModel { get; set; }

        public DbSet<Fitbod.Models.WeeklyFoodPlanModel> WeeklyFoodPlanModel { get; set; }
    }
}
