using System;
using System.Collections.Generic;
using CarInventory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace modelDB
{
    public partial class SourceContext : IdentityDbContext<SourceContextUser>
    {
        public SourceContext()
        {
        }

        public SourceContext(DbContextOptions<SourceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Make> Makes { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.HasOne(cm => cm.Make)
                      .WithMany(m => m.CarModels)
                      .HasForeignKey(cm => cm.MakeId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_CarModel_Make");
            });

            modelBuilder.Entity<Make>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
