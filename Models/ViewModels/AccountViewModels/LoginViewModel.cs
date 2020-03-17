using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebVuiVN.Models.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {

        [Display(Name = "Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { set; get; }

        [Display(Name = "Remember")]
        public bool RememberMe { set; get; }
        public string Title { set; get; }
    }
}
