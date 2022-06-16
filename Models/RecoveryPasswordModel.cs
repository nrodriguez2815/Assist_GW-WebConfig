using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class RecoveryPasswordModel
    {
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }
    }
}