using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    public class AuditModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ParentCode { get; set; }
    }
    public class AuditRiskItemModel : AuditModel
    {
        [Required]
        public string PrimaryDepartment { get; set; }
        public List<string> SecondaryDepartment { get; set; }
    }
}