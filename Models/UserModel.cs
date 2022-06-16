using System;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class UserModel
    {
        [Integer]
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Ingrese su email.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Ingrese su contraseña.")]
        public string Password { get; set; }

        [Display(Name = "Role")]
        public string Name { get; set; }

        [Display(Name = "User Type")]
        public int UserTypeId { get; set; }

        [Display(Name = "Register Date")]
        public DateTime Created { get; set; }

        public string Token_Recovery { get; set; }
    }
}