/*
The MIT License (MIT)

Copyright (c) 2007 - 2025 Microting A/S

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Microting.eFormSportFederationBase.Infrastructure.Data
{
    using eFormApi.BasePn.Abstractions;
    using eFormApi.BasePn.Infrastructure.Database.Entities;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class SportFederationPnDbContext : DbContext, IPluginDbContext
    {
        public SportFederationPnDbContext() { }

        public SportFederationPnDbContext(DbContextOptions<SportFederationPnDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressVersion> AddressVersions { get; set; }

        public DbSet<DebitBankAccount> DebitBankAccounts { get; set; }
        public DbSet<DebitBankAccountVersion> DebitBankAccountVersions { get; set; }

        public DbSet<Federation> Federations { get; set; }
        public DbSet<FederationVersion> FederationVersions { get; set; }

        // common tables
        public DbSet<PluginConfigurationValue> PluginConfigurationValues { get; set; }
        public DbSet<PluginConfigurationValueVersion> PluginConfigurationValueVersions { get; set; }
        public DbSet<PluginPermission> PluginPermissions { get; set; }
        public DbSet<PluginGroupPermission> PluginGroupPermissions { get; set; }
        public DbSet<PluginGroupPermissionVersion> PluginGroupPermissionVersions { get; set; }

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
