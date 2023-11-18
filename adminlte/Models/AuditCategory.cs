using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    [Table("AuditCategory")]
    public partial class AuditCategory
    {
        [Key]
        public string AuditCategoryCode { get; set; }

        [Required]
        public string AuditCategoryName { get; set; }

        public ICollection<AuditClass> AuditClasses { get; set; }
    }
}