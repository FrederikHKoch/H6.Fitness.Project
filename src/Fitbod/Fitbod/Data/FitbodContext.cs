using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbod.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Fitbod.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Fitbod.Data
{
    public class FitbodContext : IdentityDbContext<FitbodUser>
    {
        public FitbodContext (DbContextOptions<FitbodContext> options)
            : base(options)
        {
        }
        
        public DbSet<Exercise> Exercise { get; set; }

        public DbSet<ExercisePlan> ExercisePlan { get; set; }

        public DbSet<ExercisePlanEntry> ExercisePlanEntry { get; set; }

        public DbSet<Dish> Dish { get; set; }

        public DbSet<WeekDay> WeekDay { get; set; }

        public DbSet<WeeklyFoodPlan> WeeklyFoodPlan { get; set; }

        public DbSet<Fitbod.Models.TeamSignUp> TeamSignUp { get; set; }

        public DbSet<Fitbod.Models.TrainingClass> TrainingClass { get; set; }
        public DbSet<Areas.Identity.Data.FitbodUser> FitbodUser { get; set; }

    }
}
