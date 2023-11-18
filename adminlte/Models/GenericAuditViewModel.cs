using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace adminlte.Models
{
    public class GenericAuditViewModel<T>
    {
        public IEnumerable<T> AuditModel { get; set; }
        public IEnumerable<AuditModel> ParentAuditDataModel { get; set; }
    }
}