using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class UserTypeModel
    {
        [Display(Name = "User Type ID")]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}