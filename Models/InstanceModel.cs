using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class InstanceModel
    {
        [Display(Name = "Instance ID")]
        public int InstanceId { get; set; }

        [Display(Name = "IP Address IN")]
        public string IPAddressIn { get; set; }

        [Display(Name = "Port IN")]
        public int PortIn { get; set; }

        [Display(Name = "Api Client")]
        public string Description { get; set; }

        [Display(Name = "Api Client ID")]
        public int ApiAuthId { get; set; }
    }
}