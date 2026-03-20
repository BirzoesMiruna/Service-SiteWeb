using Microsoft.EntityFrameworkCore;
using Service.API.Models; 

namespace Service.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Serviciu> Servicii => Set<Serviciu>();
        public DbSet<Recenzie> Recenzii => Set<Recenzie>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Serviciu>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Nume).HasMaxLength(150).IsRequired();
                e.Property(x => x.Descriere).HasMaxLength(2000);
                e.Property(x => x.ImagineUrl).HasMaxLength(500);
            });

            b.Entity<Recenzie>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Text).HasMaxLength(2000).IsRequired();
                e.Property(x => x.Rating).IsRequired(); // 1–5 (validezi în controller)
                e.Property(x => x.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                e.HasIndex(x => x.ServiciuId);

                // FK catre Serviciu, sterge recenziile la delete serviciu
                e.HasOne<Serviciu>()
                 .WithMany()
                 .HasForeignKey(x => x.ServiciuId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
