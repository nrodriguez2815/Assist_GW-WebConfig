using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class RecoveryModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}