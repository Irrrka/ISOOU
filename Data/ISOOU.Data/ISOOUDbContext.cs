namespace ISOOU.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Models;
    using ISOOU.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ISOOUDbContext : IdentityDbContext<SystemUser, SystemRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ISOOUDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ISOOUDbContext(DbContextOptions<ISOOUDbContext> options)
            : base(options)
        {
        }

        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Criteria> Criterias { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<ClassProfile> ClassProfiles { get; set; }

        public DbSet<SchoolClass> SchoolClasses { get; set; }

        public DbSet<CandidateSchoolClass> CandidatesSchoolClasses { get; set; }

        public DbSet<AdmissionProcedure> AdmissionProcedures { get; set; }

        public DbSet<AddressDetails> Addresses { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Question> Questions { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Parent>()
               .HasMany(c => c.Candidates);

            builder.Entity<Candidate>()
               .HasOne(m => m.Mother);

            builder.Entity<Candidate>()
               .HasOne(m => m.Father);

            builder.Entity<SchoolClass>()
                .HasKey(fk => new { fk.SchoolId, fk.ClassId });
            //builder.Entity<SchoolClass>()
            //    .HasOne(c => c.Class)
            //    .WithMany(s => s.SchoolClasses)
            //    .HasForeignKey(s => s.ClassId);
            //builder.Entity<SchoolClass>()
            //   .HasOne(c => c.School)
            //   .WithMany(s => s.SchoolClasses)
            //   .HasForeignKey(s => s.SchoolId);

            builder.Entity<CandidateSchoolClass>()
               .HasKey(fk => new { fk.CandidateId, fk.SchoolClassId });
            //builder.Entity<CandidateSchoolClass>()
            //   .HasOne(c => c.Class)
            //   .WithMany(s => s.CandidateClasses)
            //   .HasForeignKey(s => s.SchoolClassId);
            //builder.Entity<CandidateSchoolClass>()
            //   .HasOne(c => c.Candidate)
            //   .WithMany(s => s.CandidateSchoolClasses)
            //   .HasForeignKey(s => s.CandidateId);

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<SystemUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SystemUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SystemUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
