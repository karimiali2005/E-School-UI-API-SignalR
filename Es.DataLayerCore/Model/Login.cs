using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Login
    {
        [Key]
        [Column("LoginID")]
        public int LoginId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("LoginPlatformID")]
        public int? LoginPlatformId { get; set; }
        [StringLength(50)]
        public string BrowserName { get; set; }
        [StringLength(50)]
        public string DeviceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateLogin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateExit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SessionSatrtDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SessionFinishDate { get; set; }
        [Column("IPs")]
        [StringLength(50)]
        public string Ips { get; set; }
        [StringLength(50)]
        public string ComputerName { get; set; }
        public bool? LoginSuccess { get; set; }
        [Column("IPv6")]
        [StringLength(50)]
        public string Ipv6 { get; set; }
        [StringLength(50)]
        public string DomainName { get; set; }
        [StringLength(50)]
        public string BrowserVersion { get; set; }
        [Column("appVersion")]
        public int? AppVersion { get; set; }
        [Column("mobileName")]
        [StringLength(100)]
        public string MobileName { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Column("SessionID")]
        public int? SessionId { get; set; }
        public bool? SesseionEndAuto { get; set; }
        [StringLength(50)]
        public string CookieName { get; set; }

        [ForeignKey(nameof(LoginPlatformId))]
        [InverseProperty("Login")]
        public virtual LoginPlatform LoginPlatform { get; set; }
        [ForeignKey(nameof(SessionId))]
        [InverseProperty("Login")]
        public virtual Session Session { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Login")]
        public virtual User User { get; set; }
    }
}
