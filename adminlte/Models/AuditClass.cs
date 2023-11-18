using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    [Table("AuditClass")]
    public partial class AuditClass
    {
        [Key]
        public string AuditClassCode { get; set; }

        [Required]
        public string AuditClassName { get; set; }

        [Required]
        public string AuditCategoryCode { get; set; }

        // Navigation property
        [ForeignKey("AuditCategoryCode")]
        public AuditCategory AuditCategory { get; set; }

        public ICollection<RiskItemCategory> RiskItemCategories { get; set; }
    }

}