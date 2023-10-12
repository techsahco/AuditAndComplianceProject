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
        public virtual DbSet<RiskItem> RiskItems { get; set; }

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
        }
    }
}
