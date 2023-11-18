using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    [Table("RiskItemClass")]

    public partial class RiskItemClass
    {
        [Key]
        public string RiskItemClassCode { get; set; }

        [Required]
        public string RiskItemClassName { get; set; }

        [Required]
        public string RiskItemCategoryCode { get; set; }

        // Navigation property
        [ForeignKey("RiskItemCategoryCode")]
        public RiskItemCategory RiskItemCategory { get; set; }

        public ICollection<RiskItemSubClass> RiskItemSubClasses { get; set; }

    }

}