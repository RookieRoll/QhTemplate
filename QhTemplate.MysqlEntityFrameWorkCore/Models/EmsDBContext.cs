using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class EmsDBContext : DbContext
    {
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<NewArticle> NewArticle { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SchoolArea> SchoolArea { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserOrganization> UserOrganization { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=119.28.178.12;User Id=root;Password=qh18723361304;Database=EmsDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<NewArticle>().HasQueryFilter(m => !m.IsDelete);
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ParentId).HasColumnType("int(11)");
                entity.Property(e => e.Path).HasMaxLength(255);
                entity.Property(e => e.CodeId).HasColumnType("char(36)");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BrowserInfo).HasMaxLength(256);

                entity.Property(e => e.ClientIpAddress).HasMaxLength(64);

                entity.Property(e => e.ClientName).HasMaxLength(128);

                entity.Property(e => e.Exception).HasMaxLength(2000);

                entity.Property(e => e.ExecutionDuration).HasColumnType("int(11)");

                entity.Property(e => e.ExecutionTime).HasColumnType("datetime");

                entity.Property(e => e.MethodName).HasMaxLength(256);

                entity.Property(e => e.Parameters).HasMaxLength(1024);

                entity.Property(e => e.ServiceName).HasMaxLength(256);

                entity.Property(e => e.UserId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<NewArticle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Content).HasColumnType("char(1)");

                entity.Property(e => e.IsDelete).HasColumnType("tinyint(1)");

                entity.Property(e => e.PublishTime).HasColumnType("datetime");

                entity.Property(e => e.SubContent).HasColumnType("char(1)");

                entity.Property(e => e.Title).HasColumnType("char(1)");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnType("tinyint(1)");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.Property(e => e.ParentId).HasColumnType("int(11)");

                entity.Property(e => e.Path).HasMaxLength(256);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.IsDefault).HasColumnType("tinyint(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("tinyint(1)");

                entity.Property(e => e.IsStatic).HasColumnType("tinyint(1)");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(32);
            });

            modelBuilder.Entity<SchoolArea>(entity =>
            {
                entity.HasIndex(e => e.AreaId)
                    .HasName("FK_Reference_7");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.AreaId).HasColumnType("int(11)");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.SchoolArea)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_7");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.DeletionTime).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(256);

                entity.Property(e => e.IsDeleted).HasColumnType("tinyint(1)");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(32);

                entity.Property(e => e.Password).HasMaxLength(32);

                entity.Property(e => e.UserName).HasMaxLength(32);
            });

            modelBuilder.Entity<UserOrganization>(entity =>
            {
                entity.HasKey(e => new {e.UserId, e.OrganizationId});

                entity.HasIndex(e => e.OrganizationId)
                    .HasName("FK_Reference_4");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.OrganizationId).HasColumnType("int(11)");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.UserOrganization)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOrganization)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_3");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new {e.UserId, e.RoleId});

                entity.HasIndex(e => e.RoleId)
                    .HasName("FK_Reference_2");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reference_1");
            });
        }
    }
}