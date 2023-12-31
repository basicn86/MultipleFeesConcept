using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultipleFeesConcept.Models
{
    public class Loan
    {
        public int ID = 0;
        public string borrower_name = "";
        public string address = "";
    }

    public class MortgageDbContext : DbContext
    {
        public DbSet<Loan> Loan { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=mortgage;user=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Loan>(ent =>
            {
                ent.HasKey(e => e.ID);
                ent.Property(e => e.borrower_name).IsRequired();
                ent.Property(e => e.address).IsRequired();
            });
        }
    }
}
