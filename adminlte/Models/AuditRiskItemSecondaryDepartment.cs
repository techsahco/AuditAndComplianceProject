using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace adminlte.Models
{
    [Table("AuditRiskItemSecondaryDepartment")]
    public class AuditRiskItemSecondaryDepartment
    {
        [Key, Column(Order = 0)]
        public int DepartmentId { get; set; }

        [Key, Column(Order = 1)]
        public string AuditRiskItemCode { get; set; }
    }
}