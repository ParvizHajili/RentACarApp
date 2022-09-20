using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(brand =>
            {
                brand.ToTable("Brands").HasKey(k => k.Id);
                brand.Property(p => p.Id).HasColumnName("Id");
                brand.Property(p => p.Name).HasColumnName("Name");

                brand.HasMany(p => p.Models);
            });

            modelBuilder.Entity<Model>(model =>
            {
                model.ToTable("Models").HasKey(k => k.Id);

                model.Property(p => p.Id).HasColumnName("Id");
                model.Property(p => p.BrandId).HasColumnName("BrandId");
                model.Property(p => p.Name).HasColumnName("Name");
                model.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                model.Property(p => p.ImageUrl).HasColumnName("ImageUrl");

                model.HasOne(p => p.Brand);
            });

            Brand[] brandEntitySeeds = { new(1, "BMW"), new(2, "Mercedes"), new(3, "Audi"), new(4, "Hyundai") };
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

            Model[] modelEntitySeeds = {new(1,1,"Series 5", 250,""), new(2, 1, "Series 3", 170, "")
                    , new(3, 3, "Q7", 1000, ""),new(4,4,"Sonata",550,"") };
            modelBuilder.Entity<Model>().HasData(modelEntitySeeds);
        }
    }
}
