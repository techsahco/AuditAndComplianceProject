using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    [Table("RiskItemSubClass")]

    public partial class RiskItemSubClass
    {
        [Key]
        public string RiskItemSubClassCode { get; set; }

        [Required]
        public string RiskItemSubClassName { get; set; }

        [Required]
        public string RiskItemClassCode { get; set; }

        // Navigation property
        [ForeignKey("RiskItemClassCode")]
        public RiskItemClass RiskItemClass { get; set; }

        public ICollection<AuditRiskItem> AuditRiskItems { get; set; }

    }
}