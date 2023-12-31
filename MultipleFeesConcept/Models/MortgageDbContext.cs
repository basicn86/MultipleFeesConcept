using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace MultipleFeesConcept.Models
{
    public class Loan
    {
        public int ID { get; set; } = 0;
        public string borrower_name { get; set; } = "";
        public string address { get; set; } = "";

        public virtual ObservableCollection<Fee> Fees { get; set; }
    }

    public class Fee
    {
        public int ID { get; set; }
        public int fee_type_id { get; set; }
        public int? poc_by_id { get; set; }

        public int? amount { get; set; }
        public string? payee { get; set; } = "";
        public int? poc_amount { get; set; }

        [ForeignKey("loan_id")]
        public virtual Loan Loan { get; set; }
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

            modelBuilder.Entity<Fee>(ent =>
            {
                ent.HasKey(e => e.ID);
                ent.Property(e => e.fee_type_id).IsRequired();
                ent.Property(e => e.poc_by_id);
                ent.Property(e => e.amount);
                ent.Property(e => e.payee);
                ent.Property(e => e.poc_amount);

                ent.HasOne(e => e.Loan).WithMany(e => e.Fees);
            });
        }
    }
}
