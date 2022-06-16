using System;
using System.ComponentModel.DataAnnotations;

namespace Assist_WebConfig.Models
{
    public class LogXmlModel
    {
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }

        [Display(Name = "Date Time Sent")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime LogDate { get; set; }

        [Display(Name = "Instance Name")]
        public string InstanceName { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Last Sended Xml")]
        public string Message { get; set; }

        [Display(Name = "Api Id Job")]
        public string Remarks { get; set; }
    }
}