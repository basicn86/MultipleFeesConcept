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

        public virtual ICollection<Fee> Fees { get; set; }
    }

    public class Fee
    {
        public int ID { get; set; }

        public int? amount { get; set; } = 0;
        public string? payee { get; set; } = "";
        public int? poc_amount { get; set; } = 0;

        [ForeignKey("loan_id")]
        public virtual Loan Loan { get; set; }

        [ForeignKey("fee_type_id")]
        public virtual FeeType FeeType { get; set; }
        [ForeignKey("poc_by_id")]
        public virtual PocBy? PocBy { get; set; }
    }

    [Table("fee_type")]
    public class FeeType
    {
        public int ID { get; set; }
        public string name { get; set; } = "";
    }

    [Table("poc_by")]
    public class PocBy
    {
        public int ID { get; set; }
        public string name { get; set; } = "";
    }

    public class MortgageDbContext : DbContext
    {
        public DbSet<Loan> Loan { get; set; }
        public DbSet<FeeType> FeeType { get; set; }
        public DbSet<PocBy> PocBy { get; set; }
        public DbSet<Fee> Fee { get; set; }
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
                ent.Property(e => e.amount).IsRequired();
                ent.Property(e => e.payee);
                ent.Property(e => e.poc_amount).IsRequired();

                ent.HasOne(e => e.Loan).WithMany(e => e.Fees);
                ent.HasOne(e => e.FeeType);
                ent.HasOne(e => e.PocBy);
            });

            modelBuilder.Entity<FeeType>(ent =>
            {
                ent.HasKey(e => e.ID);
                ent.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<PocBy>(ent =>
            {
                ent.HasKey(e => e.ID);
                ent.Property(e => e.name).IsRequired();
            });
        }
    }
}
