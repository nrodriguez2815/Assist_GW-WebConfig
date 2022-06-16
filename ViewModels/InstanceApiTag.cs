using Assist_WebConfig.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.ViewModels
{
    public class InstanceApiTag
    {
        [Display(Name = "Instance ID")]
        public int InstanceId { get; set; }

        [Display(Name = "IP Address IN")]
        public string IPAddressIn { get; set; }

        [Display(Name = "Port IN")]
        public int PortIn { get; set; }
        public List<GenericModel> ApiType { get; set; }
        public int Selected { get; set; }
    }
}