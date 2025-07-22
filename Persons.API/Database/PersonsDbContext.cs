using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persons.API.Database.Configuration;
using Persons.API.Database.Entities;
using Persons.API.Database.Entities.Common;
using Persons.API.Services.Interfaces;

namespace Persons.API.Database
{
    public class PersonsDbContext : IdentityDbContext<
        UserEntity,
        RoleEntity,
        string
        >
    {
        private readonly IAuditService _auditService;

        public PersonsDbContext(DbContextOptions options,
            IAuditService auditService) : base(options)
        {
            _auditService = auditService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetIdentityTablesNames(builder);

            builder.ApplyConfiguration(new CountriesConfiguration());
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified
                ));

            foreach (var entityEntry in entries)
            {
                var entity = entityEntry.Entity as BaseEntity;
                if (entity != null) 
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        entity.CreateDate = DateTime.Now;
                        entity.CreatedBy = _auditService.GetUserId();
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdatedBy = _auditService.GetUserId();
                    }
                    else
                    {
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdatedBy = _auditService.GetUserId();
                    }
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        private static void SetIdentityTablesNames(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().ToTable("sec_users");
            builder.Entity<RoleEntity>().ToTable("sec_roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("sec_users_roles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<IdentityUserClaim<string>>().ToTable("sec_users_claims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("sec_roles_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("sec_users_logins");
            builder.Entity<IdentityUserToken<string>>().ToTable("sec_users_tokens");
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<FamilyMemberEntity> FamilyGroup { get; set; }

    }
}
