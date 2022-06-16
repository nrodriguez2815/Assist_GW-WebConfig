using System.Collections.Generic;
using Assist_WebConfig.Models;

namespace Assist_WebConfig.ViewModels
{
    public class ConfigurationLogTag
    {
        public int InstanceId { get; set; }
        public int VehiclesListCheckTimer { get; set; }
        public int XmlsSenderTimer { get; set; }
        public List<GenericModel> LogType { get; set; }
        public int Selected { get; set; }
        public bool ActivityLog { get; set; }
        public string LogPath { get; set; }
        public int Retries { get; set; }
        public string InstanceName { get; set; }
    }
}