using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Data.Entities;

namespace WebVuiVN.Models.ViewModels.ProfileViewModels
{
    public class ProfileViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public string StatusMessage { get; set; }
        public string Avatar { get; set; }
        public string BirthDay { set; get; }
        public string About { set; get; }
    }
}
