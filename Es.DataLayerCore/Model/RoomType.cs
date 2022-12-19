using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class RoomType
    {
        public RoomType()
        {
            Room = new HashSet<Room>();
        }

        [Key]
        [Column("RoomTypeID")]
        public int RoomTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoomTypeTitle { get; set; }

        [InverseProperty("RoomType")]
        public virtual ICollection<Room> Room { get; set; }
    }
}
