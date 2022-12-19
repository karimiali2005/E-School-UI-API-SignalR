using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class RoomType
    {
        public RoomType()
        {
            Room = new HashSet<Room>();
        }

        public int RoomTypeId { get; set; }
        public string RoomTypeTitle { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
