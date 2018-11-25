using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi_basic_mysql.EntityConfigurations;
using webapi_basic_mysql.Models;

namespace webapi_basic_mysql.Persistence
{
    public class DataContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new UserConfigurations());
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess){
           OnBeforeSaving();
           return base.SaveChanges(acceptAllChangesOnSuccess);
       }

       public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken)) {
           OnBeforeSaving();
           return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
       }

       private void OnBeforeSaving() {
           var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

           //var currentUsername = “Write logic to get logged in user here”;

           var now = DateTime.UtcNow;

           foreach (var entity in entities)
           {
               var baseEntity = ((BaseEntity) entity.Entity);

               switch(entity.State) {
                   case EntityState.Added:
                       baseEntity.CreatedAt = now;
                       baseEntity.ModifiedAt = now;
                       break;

                   case EntityState.Modified:
                       baseEntity.ModifiedAt = now;
                       break;
               }
           }
       }
    }
}