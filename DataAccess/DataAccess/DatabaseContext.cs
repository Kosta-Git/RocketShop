using System;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext( DbContextOptions options ) : base( options ) { }


        public override int SaveChanges( bool acceptAllChangesOnSuccess )
        {
            OnBeforeSaving();
            return base.SaveChanges( acceptAllChangesOnSuccess );
        }

        public override Task<int> SaveChangesAsync( bool acceptAllChangesOnSuccess,
                                                    CancellationToken cancellationToken = new CancellationToken() )
        {
            OnBeforeSaving();
            return base.SaveChangesAsync( acceptAllChangesOnSuccess, cancellationToken );
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow  = DateTime.UtcNow; // Get utc now before iterating, so we get the same time for every entity

            foreach ( var entry in entries )
            {
                // Automatically update created at and updated at
                BaseEntityHandler( entry, utcNow );
            }
        }

        private void BaseEntityHandler( EntityEntry entry, DateTime utcNow )
        {
            if ( entry.Entity is BaseEntity baseEntity )
            {
                switch ( entry.State )
                {
                case EntityState.Modified:
                    // If it was modified update last updated at, and do not touch created on
                    baseEntity.LastUpdatedAt                 = utcNow;
                    entry.Property( "CreatedOn" ).IsModified = false;
                    break;

                case EntityState.Added:
                    // If the entity was just added we need to set updated at and created at
                    baseEntity.CreatedAt     = utcNow;
                    baseEntity.LastUpdatedAt = utcNow;
                    break;
                }
            }
        }

        public DbSet<Coin> Coins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Validation> Validations { get; set; }
        public DbSet<ValidationRule> ValidationRules { get; set; }
    }
}