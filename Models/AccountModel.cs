using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class AccountModel
    {
        [Display(Name = "Instance Id")]
        public int InstanceId { get; set; }

        [Display(Name = "Account Id")]
        [Required(ErrorMessage = "This field is required")]
        public int AccountId { get; set; }
    }
}