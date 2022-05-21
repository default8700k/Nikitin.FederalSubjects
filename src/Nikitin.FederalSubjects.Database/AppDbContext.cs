using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Database.Models.DbModels;

namespace Nikitin.FederalSubjects.Database
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<FederalDistrict> FederalDistricts { get; set; } = null!;
        public virtual DbSet<FederalSubject> FederalSubjects { get; set; } = null!;
        public virtual DbSet<FederalSubjectType> FederalSubjectTypes { get; set; } = null!;
        public virtual DbSet<Map> Maps { get; set; } = null!;

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FederalDistrict>(entity =>
            {
                entity.ToTable("federal_districts", "models");

                entity.HasIndex(e => e.Name, "federal_districts_name_key").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(128);
            });

            modelBuilder.Entity<FederalSubject>(entity =>
            {
                entity.ToTable("federal_subjects", "models");

                entity.HasIndex(e => e.Name, "federal_subjects_name_key").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FederalDistrictId).HasColumnName("federal_district_id");
                entity.Property(e => e.FederalSubjectTypeId).HasColumnName("federal_subject_type_id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(128);
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Content).HasColumnName("content");

                entity.HasOne(d => d.FederalDistrict)
                    .WithMany(p => p.FederalSubjects)
                    .HasForeignKey(d => d.FederalDistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("federal_subjects_federal_district_id_fkey");

                entity.HasOne(d => d.FederalSubjectType)
                    .WithMany(p => p.FederalSubjects)
                    .HasForeignKey(d => d.FederalSubjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("federal_subjects_federal_subject_type_id_fkey");
            });

            modelBuilder.Entity<FederalSubjectType>(entity =>
            {
                entity.ToTable("federal_subject_types", "models");

                entity.HasIndex(e => e.Name, "federal_subject_types_name_key").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(128);
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.ToTable("map", "models");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FederalSubjectId).HasColumnName("federal_subject_id");
                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.FederalSubject)
                    .WithMany(p => p.Maps)
                    .HasForeignKey(d => d.FederalSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("map_federal_subject_id_fkey");
            });
        }
    }
}
