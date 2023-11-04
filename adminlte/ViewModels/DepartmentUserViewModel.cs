using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class DepartmentUserRequestViewModel
    {

        [Required]
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}