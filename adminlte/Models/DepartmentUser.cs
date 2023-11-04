namespace adminlte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepartmentUser")]
    public partial class DepartmentUser
    {
        [Key, Column(Order = 0)]
        public int DepartmentID { get; set; }
        [Key, Column(Order = 1)]
        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
