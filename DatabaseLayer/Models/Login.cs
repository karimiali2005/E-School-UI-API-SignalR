using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public int? UserId { get; set; }
        public int? LoginPlatformId { get; set; }
        public string BrowserName { get; set; }
        public string DeviceName { get; set; }
        public DateTime? DateLogin { get; set; }
        public DateTime? DateExit { get; set; }
        public string Ips { get; set; }
        public string ComputerName { get; set; }
        public bool? LoginSuccess { get; set; }
        public string Ipv6 { get; set; }
        public string DomainName { get; set; }
        public string BrowserVersion { get; set; }
        public int? AppVersion { get; set; }
        public string MobileName { get; set; }
        public string Description { get; set; }

        //public virtual BrowserType BrowserType { get; set; }
        //public virtual DeviceType DeviceType { get; set; }
        public virtual LoginPlatform LoginPlatform { get; set; }
        public virtual User User { get; set; }
    }
}
