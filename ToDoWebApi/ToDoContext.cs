using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Models;

namespace ToDoWebApi
{
    public class ToDoContext : DbContext
    {

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDos {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ToDoItem>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<ToDoItem>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ToDoItem>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override int SaveChanges()
        {
            SetTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void SetTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entityEntry in entities)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).DateCreated = DateTime.UtcNow;
                }

                ((BaseEntity)entityEntry.Entity).DateUpdated = DateTime.UtcNow;
            }
        }
    }
}
