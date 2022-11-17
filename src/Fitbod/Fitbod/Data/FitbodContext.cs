using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbod.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Fitbod.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Fitbod.Data
{
    public class FitbodContext : IdentityDbContext<FitbodUser>
    {
        public FitbodContext(DbContextOptions<FitbodContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
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

    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Exercise>().HasData(
                   new Exercise() { ExerciseId = 1, Name = "Squat", Musclegroup = "Baglår", Description = "Adddescriptionhere", Image = "Squat.img" },
                   new Exercise() { ExerciseId = 2, Name = "Deadlifts", Musclegroup = "Baglår", Description = "Adddescriptionhere", Image = "Deadlift.img" },
                   new Exercise() { ExerciseId = 3, Name = "Jump rope", Musclegroup = "Læg", Description = "Adddescriptionhere", Image = "Jump.img" },
                   new Exercise() { ExerciseId = 4, Name = "Dumbbell jump squat", Musclegroup = "Læg", Description = "Adddescriptionhere", Image = "jump2.img" },
                   new Exercise() { ExerciseId = 5, Name = "Bench press", Musclegroup = "Bryst", Description = "Adddescriptionhere", Image = "benchpress.img" },
                   new Exercise() { ExerciseId = 6, Name = "Seated Cable Chest Press", Musclegroup = "Bryst", Description = "Adddescriptionhere", Image = "vroom.img" },
                   new Exercise() { ExerciseId = 7, Name = "Chest-supported Row", Musclegroup = "Ryg", Description = "Adddescriptionhere", Image = "row.img" },
                   new Exercise() { ExerciseId = 8, Name = "Pull-ups", Musclegroup = "Ryg", Description = "Adddescriptionhere", Image = "pullup.img" },
                   new Exercise() { ExerciseId = 9, Name = "Overhead press", Musclegroup = "Skulder", Description = "Adddescriptionhere", Image = "shoulder.img" },
                   new Exercise() { ExerciseId = 10, Name = "Reverse grip", Musclegroup = "Triceps", Description = "Adddescriptionhere", Image = "reverse.img" },
                   new Exercise() { ExerciseId = 11, Name = "Rope Triceps Pressdown", Musclegroup = "Triceps", Description = "Adddescriptionhere", Image = "trex.img" },
                   new Exercise() { ExerciseId = 12, Name = "Close grip pull-up", Musclegroup = "Biceps", Description = "Adddescriptionhere", Image = "closedgrippullup.img" },
                   new Exercise() { ExerciseId = 13, Name = "Dumbbell Curls", Musclegroup = "Biceps", Description = "Adddescriptionhere", Image = "bellcurls.img" },
                   new Exercise() { ExerciseId = 14, Name = "Decline Crunch", Musclegroup = "Mavemuskler", Description = "Adddescriptionhere", Image = "crunch.img" }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Bruger", NormalizedName = "BRUGER" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Superbruger", NormalizedName = "SUPERBRUGER" }
            );
        }
    }
}
