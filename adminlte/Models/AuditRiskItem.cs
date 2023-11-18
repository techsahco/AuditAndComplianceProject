using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    [Table("AuditRiskItem")]
    public class AuditRiskItem
    {
        [Key]
        public string RiskItemCode { get; set; }

        [Required]
        public string RiskItemName { get; set; }

        [Required]
        public string RiskItemSubClassCode { get; set; }

        // Navigation property
        [ForeignKey("RiskItemSubClassCode")]
        public RiskItemSubClass RiskItemSubClass { get; set; }
    }
}