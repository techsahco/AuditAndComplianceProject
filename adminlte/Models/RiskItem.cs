namespace adminlte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RiskItem")]
    public partial class RiskItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RiskItemID { get; set; }

        [StringLength(255)]
        public string RiskItemName { get; set; }

        [StringLength(20)]
        public string Priority { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
