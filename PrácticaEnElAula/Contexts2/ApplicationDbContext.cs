using Microsoft.EntityFrameworkCore;
using PrácticaEnElAula.Models;
using System;

namespace PrácticaEnElAula.Contexts2
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Vacations> Vacation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(employee => employee.Id);

            modelBuilder.Entity<Vacations>().HasKey(Vacations => Vacations.Id);

            modelBuilder.Entity<Employee>().HasMany(Employee => Employee.Vacations)
                .WithOne(Vacation => Vacation.Employee)
                .HasForeignKey(Vacation => Vacation.IdEmployee)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
