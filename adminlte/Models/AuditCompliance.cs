namespace adminlte.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AuditCompliance : DbContext
    {
        public AuditCompliance()
            : base("name=AuditCompliance")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentUser> DepartmentUser { get; set; }
        public virtual DbSet<RiskItem> RiskItems { get; set; }


        // New DbSet properties for AuditCategory and AuditClass
        public virtual DbSet<AuditCategory> AuditCategories { get; set; }
        public virtual DbSet<AuditClass> AuditClasses { get; set; }
        public virtual DbSet<RiskItemCategory> RiskItemCategories { get; set; }
        public virtual DbSet<RiskItemClass> RiskItemClasses { get; set; }
        public virtual DbSet<RiskItemSubClass> RiskItemSubClasses { get; set; }
        public virtual DbSet<AuditRiskItem> AuditRiskItems { get; set; }
        public virtual DbSet<AuditRiskItemSecondaryDepartment> AuditRiskItemSecondaryDepartment { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItem>()
                .Property(e => e.RiskItemName)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItem>()
                .Property(e => e.Priority)
                .IsUnicode(false);

            // Configure the AuditCategory entity
            modelBuilder.Entity<AuditCategory>()
                .HasKey(e => e.AuditCategoryCode);

            modelBuilder.Entity<AuditCategory>()
                .Property(e => e.AuditCategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<AuditCategory>()
                .Property(e => e.AuditCategoryName)
                .IsUnicode(false);

            // Configure the AuditClass entity
            modelBuilder.Entity<AuditClass>()
                .HasKey(e => e.AuditClassCode);

            modelBuilder.Entity<AuditClass>()
                .Property(e => e.AuditClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<AuditClass>()
                .Property(e => e.AuditClassName)
                .IsUnicode(false);

            modelBuilder.Entity<AuditClass>()
                .Property(e => e.AuditCategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<AuditClass>()
                .HasRequired(e => e.AuditCategory)
                .WithMany(e => e.AuditClasses)
                .HasForeignKey(e => e.AuditCategoryCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RiskItemCategory>()
                .HasKey(e => e.RiskItemCategoryCode);

            modelBuilder.Entity<RiskItemCategory>()
                .Property(e => e.RiskItemCategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemCategory>()
                .Property(e => e.RiskItemCategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemCategory>()
                .Property(e => e.AuditClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemCategory>()
                .HasRequired(e => e.AuditClass)
                .WithMany(e => e.RiskItemCategories)
                .HasForeignKey(e => e.AuditClassCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RiskItemClass>()
                .HasKey(e => e.RiskItemClassCode);

            modelBuilder.Entity<RiskItemClass>()
                .Property(e => e.RiskItemClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemClass>()
                .Property(e => e.RiskItemClassName)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemClass>()
                .Property(e => e.RiskItemCategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemClass>()
                .HasRequired(e => e.RiskItemCategory)
                .WithMany(e => e.RiskItemClasses)
                .HasForeignKey(e => e.RiskItemCategoryCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RiskItemSubClass>()
                .HasKey(e => e.RiskItemSubClassCode);

            modelBuilder.Entity<RiskItemSubClass>()
                .Property(e => e.RiskItemSubClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemSubClass>()
                .Property(e => e.RiskItemSubClassName)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemSubClass>()
                .Property(e => e.RiskItemClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<RiskItemSubClass>()
                .HasRequired(e => e.RiskItemClass)
                .WithMany(e => e.RiskItemSubClasses)
                .HasForeignKey(e => e.RiskItemClassCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AuditRiskItem>()
                .HasKey(e => e.RiskItemCode);

            modelBuilder.Entity<AuditRiskItem>()
                .Property(e => e.RiskItemCode)
                .IsUnicode(false);

            modelBuilder.Entity<AuditRiskItem>()
                .Property(e => e.RiskItemName)
                .IsUnicode(false);

            modelBuilder.Entity<AuditRiskItem>()
                .Property(e => e.RiskItemSubClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<AuditRiskItem>()
                .HasRequired(e => e.RiskItemSubClass)
                .WithMany(e => e.AuditRiskItems)
                .HasForeignKey(e => e.RiskItemSubClassCode)
                .WillCascadeOnDelete(false);
        }

    }
}
