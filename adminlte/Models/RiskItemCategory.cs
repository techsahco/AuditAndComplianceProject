using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    [Table("RiskItemCategory")]
    public partial class RiskItemCategory
    {
        [Key]
        public string RiskItemCategoryCode { get; set; }

        [Required]
        public string RiskItemCategoryName { get; set; }

        [Required]
        public string AuditClassCode { get; set; }

        // Navigation property
        [ForeignKey("AuditClassCode")]
        public AuditClass AuditClass { get; set; }


        public ICollection<RiskItemClass> RiskItemClasses { get; set; }
    }

}