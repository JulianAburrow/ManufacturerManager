using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ManufacturerManager.DataAccess
{
    public class ManufacturerManagerContext : DbContext
    {
        public int UserId { get; set; }

        public ManufacturerManagerContext(DbContextOptions<ManufacturerManagerContext> options)
            : base(options)
        {
        }

        #region DbSets

        public DbSet<ColourJustification> ColourJustification { get; set; }

        public DbSet<Manufacturer> Manufacturer { get; set; }

        public DbSet<ManufacturerStatus> ManufacturerStatus { get; set; }

        public DbSet<StaffMember> StaffMember { get; set; }

        public DbSet<Widget> Widget { get; set; }

        public DbSet<Colour> Colour { get; set; }

        public DbSet<WidgetStatus> WidgetStatus { get; set; }

        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ColourJustificationConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerStatusConfiguration());
            modelBuilder.ApplyConfiguration(new StaffMemberConfiguration());
            modelBuilder.ApplyConfiguration(new WidgetConfiguration());
            modelBuilder.ApplyConfiguration(new ColourConfiguration());
            modelBuilder.ApplyConfiguration(new WidgetStatusConfiguration());
        }

        #endregion

        #region SaveChangesAsync

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        if (changedEntity.Entity is IAuditableObject objAdded)
                        {
                            objAdded.Created = DateTime.Now;
                            objAdded.CreatedById = UserId;
                            objAdded.LastUpdated = DateTime.Now;
                            objAdded.LastUpdatedById = UserId;
                        }
                        break;
                    case EntityState.Modified:
                        if (changedEntity.Entity is IAuditableObject objModified)
                        {
                            objModified.LastUpdated = DateTime.Now;
                            objModified.LastUpdatedById = UserId;
                        }
                        break;
                }
            }

            return await base.SaveChangesAsync();
        }

        #endregion
    }
}
