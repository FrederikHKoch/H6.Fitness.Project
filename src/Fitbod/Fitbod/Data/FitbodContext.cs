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

        public DbSet<Exercise> Exercise { get; set; }

        public DbSet<ExercisePlan> ExercisePlan { get; set; }

        public DbSet<ExercisePlanEntry> ExercisePlanEntry { get; set; }

        public DbSet<User> User { get; set; } = default!;

        public DbSet<Role> Role { get; set; }

        public DbSet<Dish> Dish { get; set; }

        public DbSet<WeekDay> WeekDay { get; set; }

        public DbSet<WeeklyFoodPlan> WeeklyFoodPlan { get; set; }

    }
}
