using Produtos.Domain.Providers;
using Microsoft.EntityFrameworkCore;

namespace Produtos.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DbSettingsProvider _dbSettings;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AppConfig appConfig)
            : base(options)
        {
            _dbSettings = appConfig.DbContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_dbSettings.AppConnectionString, options =>
                {
                    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }

            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Model.GetEntityTypes().ToList().ForEach(entityType =>
            {
                entityType.SetTableName(entityType.DisplayName());

                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);

                entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(string))
                    .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name))
                    .ToList()
                    .ForEach(property =>
                    {
                        property.IsUnicode(false);
                        property.HasMaxLength(DefaultValuesProvider.VarcharLenght);
                    });
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
