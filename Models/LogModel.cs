using System;
using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class LogModel
    {
        [Display(Name = "Instance ID")]
        public int InstanceId { get; set; }

        [Display(Name = "Log Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime LogDate { get; set; }

        public string Message { get; set; }

        [Display(Name = "Instance Name")]
        public string InstanceName { get; set; }
    }
}