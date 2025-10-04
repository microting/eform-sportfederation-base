using Microsoft.EntityFrameworkCore;
using Microting.eFormSportFederationBase.Data.Entities;

namespace Microting.eFormSportFederationBase.Data
{
    public class SportFederationDbContext : DbContext
    {
        public SportFederationDbContext(DbContextOptions<SportFederationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<DebitBankAccount> DebitBankAccounts { get; set; }
        public DbSet<Federation> Federations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Federation relationships
            modelBuilder.Entity<Federation>()
                .HasOne(f => f.ContactAddress)
                .WithMany()
                .HasForeignKey(f => f.ContactAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Federation>()
                .HasOne(f => f.BillingAddress)
                .WithMany()
                .HasForeignKey(f => f.BillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Federation>()
                .HasOne(f => f.DebitBankAccount)
                .WithMany()
                .HasForeignKey(f => f.DebitBankAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
