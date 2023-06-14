using Microsoft.EntityFrameworkCore;
using PermissionManagement.Model.Entities;
using PermissionManagement.Model.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionManagement.Model.Context
{
    public class PermissionContext : DbContext
    {
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }
        public PermissionContext(DbContextOptions options) : base(options)
        {
        }
        public PermissionContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasMany<PermissionType>().WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }
        public override int SaveChanges()
        {
            // Get the entries that are auditable
            var entries = ChangeTracker.Entries<IAuditEntity>();

            if (entries == null)
                return base.SaveChanges();

            var userId = Guid.NewGuid().ToString();
            var currentDate = DateTime.Now;

            var validStates = new HashSet<EntityState> { EntityState.Added, EntityState.Modified, EntityState.Deleted };
            var entriesFiltered = entries.Where(x => validStates.Contains(x.State));

            foreach (var entry in entriesFiltered)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = currentDate;
                        entry.Entity.CreatedBy = userId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = currentDate;
                        entry.Entity.UpdatedBy = userId;
                        break;
                    case EntityState.Deleted:
                        ((ISoftDeleteEntity)entry.Entity).IsDeleted = true;
                        entry.State = EntityState.Modified;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
