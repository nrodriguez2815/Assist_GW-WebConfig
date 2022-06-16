using Assist_WebConfig.Models;
using DataAnnotationsExtensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.ViewModels
{
    public class UserTypeTag
    {
        [Integer]
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Ingrese su email.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public List<GenericModel> UserType { get; set; }
        public int Selected { get; set; }
    }
}