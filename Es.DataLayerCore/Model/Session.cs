using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Session
    {
        public Session()
        {
            Login = new HashSet<Login>();
        }

        [Key]
        [Column("SessionID")]
        public int SessionId { get; set; }
        [StringLength(50)]
        public string Ticket { get; set; }
        [Column("LoginPlatformID")]
        public int? LoginPlatformId { get; set; }
        [StringLength(50)]
        public string BrowserName { get; set; }
        [StringLength(50)]
        public string DeviceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SessionSatrtDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SessionFinishDate { get; set; }
        [Column("IPs")]
        [StringLength(50)]
        public string Ips { get; set; }
        [StringLength(50)]
        public string BrowserVersion { get; set; }
        public bool? SesseionEndAuto { get; set; }
        [StringLength(50)]
        public string CookieName { get; set; }

        [InverseProperty("Session")]
        public virtual ICollection<Login> Login { get; set; }
    }
}
