using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminlte.ViewModels
{
    public class DepartmentUserViewModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public bool IsPrimary { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}