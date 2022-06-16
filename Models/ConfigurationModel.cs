using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Assist_WebConfig.Models
{
    public class ConfigurationModel
    {
        public int InstanceId { get; set; }

        [Display(Name = "Vehicles Timer")]
        [Required(ErrorMessage = "This field is required.")]
        public int VehiclesListCheckTimer { get; set; }

        [Display(Name = "Sender Timer")]
        [Required(ErrorMessage = "This field is required.")]
        public int XmlsSenderTimer { get; set; }

        [Display(Name = "Log Type")]
        [Required]
        public int LogTypeId { get; set; }

        [Display(Name = "Logs")]
        public string Description { get; set; }

        [Display(Name = "Path")]
        public string LogPath { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int Retries { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string InstanceName { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Installed")]
        public DateTime? InstallationDate { get; set; }
    }
}